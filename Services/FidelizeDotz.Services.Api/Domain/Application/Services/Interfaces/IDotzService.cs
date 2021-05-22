using System;
using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Dotz;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Dotz;
using FidelizeDotz.Services.Api.Domain.Entities;

namespace FidelizeDotz.Services.Api.Domain.Application.Services.Interfaces
{
    public interface IDotzService
    {
        Task<ReturnMessage> InsertDotAsync(InsertDotRequest request);
        Task<ReturnMessage<IPagedList<DotResponse>>> GetAllDotsAsync(GetAllDotRequest request);
        Task<ReturnMessage<DotResponse>> RescuedDot(RescuedDotRequest request);
        Task<ReturnMessage<DotBalanceResponse>> GetBalanceDotsAsync();
    }
}