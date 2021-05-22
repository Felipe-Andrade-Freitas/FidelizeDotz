using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.User;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.User;
using FidelizeDotz.Services.Api.Domain.Application.Services.Interfaces;
using FidelizeDotz.Services.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace FidelizeDotz.Services.Api.Controllers
{
    [ApiVersion("1.0")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        ///     Login of the user
        /// </summary>
        /// <response code="200">Return of the Token (JWT)</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ReturnMessage<IPagedList<UserResponse>>), Status200OK)]
        [ProducesResponseType(typeof(ReturnMessage), Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnMessage), Status500InternalServerError)]
        [HttpPost("login")]
        public Task<ReturnMessage<UserResponse>> LoginUserAsync([FromBody] LoginUserRequest request)
        {
            return _userService.LoginUserAsync(request);
        }

        /// <summary>
        ///     Register a new user
        /// </summary>
        /// <response code="200">Return of the Token (JWT)</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ReturnMessage<IPagedList<UserResponse>>), Status200OK)]
        [ProducesResponseType(typeof(ReturnMessage), Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnMessage), Status500InternalServerError)]
        [HttpPost("register")]
        public Task<ReturnMessage<UserResponse>> RegisterUserAsync([FromBody] RegisterUserRequest request)
        {
            return _userService.RegisterUserAsync(request);
        }
    }
}