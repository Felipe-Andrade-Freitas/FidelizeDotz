using System;
using System.Linq;
using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Bases;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Application.Dtos;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Dotz;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Dotz;
using FidelizeDotz.Services.Api.Domain.Application.Services.Interfaces;
using FidelizeDotz.Services.Api.Domain.Entities;
using FidelizeDotz.Services.Api.Domain.Infra.Interfaces;
using static FidelizeDotz.Services.Api.CrossCutting.Constants.ErrorsConstants;
using static FidelizeDotz.Services.Api.Domain.Enums.EReturnMessageType;

namespace FidelizeDotz.Services.Api.Domain.Application.Services
{
    public class DotzService : ServiceBase, IDotzService
    {
        public DotzService(IUnitOfWork unitOfWork, IAdapter adapter, UserLogged userLogged) : base(unitOfWork, adapter,
            userLogged: userLogged)
        {
        }

        public async Task<ReturnMessage> InsertDotAsync(InsertDotRequest request)
        {
            try
            {
                if (request is null)
                    return new ReturnMessage(RequestIsNull, ClientErrorBadRequest);

                var dot = Adapter.ConvertTo<InsertDotRequest, Dot>(request);
                dot.UserId = UserLogged.Id;
                await UnitOfWork.GetRepository<Dot>().InsertAsync(dot);
                await UnitOfWork.SaveChangesAsync();

                return new ReturnMessage(true, SuccessCreated);
            }
            catch (Exception)
            {
                return new ReturnMessage(UnexpectedError, ClientErrorBadRequest);
            }
        }

        public async Task<ReturnMessage<IPagedList<DotResponse>>> GetAllDotsAsync(GetAllDotRequest request)
        {
            try
            {
                if (request is null)
                    return new ReturnMessage<IPagedList<DotResponse>>(RequestIsNull, ClientErrorBadRequest);

                var result = await UnitOfWork.GetRepository<Dot>()
                    .GetPagedListAsync(pageIndex: request.PageIndex, pageSize: request.PageSize);
                return new ReturnMessage<IPagedList<DotResponse>>(
                    Adapter.ConvertTo<PagedList<Dot>, PagedList<DotResponse>>((PagedList<Dot>) result));
            }
            catch (Exception)
            {
                return new ReturnMessage<IPagedList<DotResponse>>(UnexpectedError, ClientErrorBadRequest);
            }
        }

        public async Task<ReturnMessage<DotResponse>> RescuedDot(RescuedDotRequest request)
        {
            try
            {
                if (request is null)
                    return new ReturnMessage<DotResponse>(RequestIsNull, ClientErrorBadRequest);

                var balance = await GetBalanceDotsAsync();
                if (balance.Data.Balance == 0 || balance.Data.Balance <= request.Quantity)
                    return new ReturnMessage<DotResponse>(InsufficientDots, ClientErrorBadRequest);

                var dot = Adapter.ConvertTo<RescuedDotRequest, Dot>(request);
                dot.Quantity *= -1;
                dot.UserId = UserLogged.Id;
                await UnitOfWork.GetRepository<Dot>().InsertAsync(dot);
                await UnitOfWork.SaveChangesAsync();
            
                return new ReturnMessage<DotResponse>(
                    Adapter.ConvertTo<Dot, DotResponse>(dot));
            }
            catch (Exception)
            {
                return new ReturnMessage<DotResponse>(UnexpectedError, ClientErrorBadRequest);
            }
        }

        public async Task<ReturnMessage<DotBalanceResponse>> GetBalanceDotsAsync()
        {
            try
            {
                var balance = UnitOfWork.GetRepository<Dot>()
                    .GetAll().Where(_ => _.UserId == UserLogged.Id).Sum(_ => _.Quantity);
                var result = new ReturnMessage<DotBalanceResponse>(new DotBalanceResponse
                {
                    Balance = balance
                });
                return result;
            }
            catch (Exception)
            {
                return new ReturnMessage<DotBalanceResponse>(UnexpectedError, ClientErrorBadRequest);
            }
        }
    }
}