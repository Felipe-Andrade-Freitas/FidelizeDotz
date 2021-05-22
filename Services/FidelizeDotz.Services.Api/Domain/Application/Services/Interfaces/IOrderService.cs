using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Order;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Order;
using FidelizeDotz.Services.Api.Domain.Entities;

namespace FidelizeDotz.Services.Api.Domain.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ReturnMessage<IPagedList<OrderResponse>>> ListOrderForUser(ListOrderForUserRequest request);
    }
}