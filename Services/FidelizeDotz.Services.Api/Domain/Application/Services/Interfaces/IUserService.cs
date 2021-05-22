using System.Threading.Tasks;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.User;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.User;
using FidelizeDotz.Services.Api.Domain.Entities;

namespace FidelizeDotz.Services.Api.Domain.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<ReturnMessage<UserResponse>> LoginUserAsync(LoginUserRequest request);
        Task<ReturnMessage<UserResponse>> RegisterUserAsync(RegisterUserRequest request);
        Task<ReturnMessage> InsertAddressAsync(InsertAddressRequest request);
    }
}