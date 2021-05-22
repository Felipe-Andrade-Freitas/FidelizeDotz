using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Bases;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Application.Dtos;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Order;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Order;
using FidelizeDotz.Services.Api.Domain.Application.Services.Interfaces;
using FidelizeDotz.Services.Api.Domain.Entities;
using FidelizeDotz.Services.Api.Domain.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FidelizeDotz.Services.Api.Domain.Application.Services
{
    public class OrderService : ServiceBase, IOrderService
    {
        public OrderService(IUnitOfWork unitOfWork, IAdapter adapter, UserLogged userLogged) : base(unitOfWork, adapter, userLogged: userLogged)
        {
            
        }

        public async Task<ReturnMessage<IPagedList<OrderResponse>>> ListOrderForUser(ListOrderForUserRequest request)
        {
            var orders = await UnitOfWork.GetRepository<Order>()
                .GetPagedListAsync(_ => _.UserId == UserLogged.Id, pageIndex: request.PageIndex, pageSize: request.PageSize, 
                    include: _ => _.Include(__ => __.Deliveries)
                        .ThenInclude(_ => _.Address)
                        .Include(__ => __.OrderItems)
                        .Include(__ => __.Payments));
            return new ReturnMessage<IPagedList<OrderResponse>>(
                Adapter.ConvertTo<PagedList<Order>, PagedList<OrderResponse>>((PagedList<Order>)orders));
        }
    }
}