using System;
using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Product;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Product;
using FidelizeDotz.Services.Api.Domain.Entities;

namespace FidelizeDotz.Services.Api.Domain.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ReturnMessage> InsertProductAsync(InsertProductRequest request);
        Task<ReturnMessage<IPagedList<ProductResponse>>> ListProductsAvailableForRedemptionAsync();
        Task<ReturnMessage> RescuedProductAsync(Guid id);
    }
}