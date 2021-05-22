using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Category;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Category;
using FidelizeDotz.Services.Api.Domain.Entities;

namespace FidelizeDotz.Services.Api.Domain.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ReturnMessage> InsertCategoryAsyc(InsertCategoryRequest request);
        Task<ReturnMessage<IPagedList<CategoryResponse>>> ListCategoriesAsync();
    }
}