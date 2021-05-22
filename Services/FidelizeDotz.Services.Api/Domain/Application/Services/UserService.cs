using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Bases;
using FidelizeDotz.Services.Api.Domain.Application.Dtos;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.User;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.User;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Settings;
using FidelizeDotz.Services.Api.Domain.Application.Services.Interfaces;
using FidelizeDotz.Services.Api.Domain.Entities;
using FidelizeDotz.Services.Api.Domain.Enums;
using FidelizeDotz.Services.Api.Domain.Infra.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FidelizeDotz.Services.Api.Domain.Application.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, IAdapter adapter, SignInManager<User> signInManager, UserManager<User> userManager,
            AppSettings appSettings, UserLogged userLogged) : base(unitOfWork, adapter, userLogged: userLogged)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings;
        }


        public async Task<ReturnMessage<UserResponse>> RegisterUserAsync(RegisterUserRequest request)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.Email,
                EmailConfirmed = true,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return new ReturnMessage<UserResponse>(result.Errors.Select(_ => _.Description));

            user = await _userManager.FindByNameAsync(request.Email);
            var claimsResult = await _userManager.AddClaimsAsync(user, await CreateUserClaims(user.Id, request));

            if (!claimsResult.Succeeded)
                return new ReturnMessage<UserResponse>(
                    "The information has not been fully registered, please contact support.");

            if (result.Succeeded)
                return new ReturnMessage<UserResponse>(await GenerateJwt(request.Email, user));

            return new ReturnMessage<UserResponse>(result.Errors.Select(_ => _.Description));
        }

        public async Task<ReturnMessage<UserResponse>> LoginUserAsync(LoginUserRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password,
                false, true);

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return new ReturnMessage<UserResponse>("User not found.");

            if (result.Succeeded)
                return new ReturnMessage<UserResponse>(await GenerateJwt(request.UserName));

            if (result.IsLockedOut)
                return new ReturnMessage<UserResponse>("Usuário temporariamente bloqueado por tentativas inválidas",
                    EReturnMessageType.ClientErrorBadRequest);

            return new ReturnMessage<UserResponse>("Usuário ou Senha incorretos",
                EReturnMessageType.ClientErrorBadRequest);
        }

        public async Task<ReturnMessage> InsertAddressAsync(InsertAddressRequest request)
        {
            var address = Adapter.ConvertTo<InsertAddressRequest, Address>(request);
            address.UserId = UserLogged.Id;
            await UnitOfWork.GetRepository<Address>().InsertAsync(address);
            await UnitOfWork.SaveChangesAsync();

            return new ReturnMessage(true, EReturnMessageType.SuccessCreated);
        }

        #region [ Private ]

        private async Task<UserResponse> GenerateJwt(string userName, User user = null)
        {
            user ??= await _userManager.FindByNameAsync(userName);
            var claims = new ClaimsIdentity(await _userManager.GetClaimsAsync(user));
            var encodedToken = EncryptToken(claims);

            return new UserResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.TimeExpiration).TotalSeconds
            };
        }

        private string EncryptToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Valid,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.TimeExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }


        private async Task<IList<Claim>> CreateUserClaims(Guid id, RegisterUserRequest request)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, id.ToString()));
            claims.Add(new Claim("name", request.Name));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, request.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(),
                ClaimValueTypes.Integer64));

            return claims;
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long) Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
        }

        #endregion
    }
}