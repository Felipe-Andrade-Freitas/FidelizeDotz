using System;
using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Bases;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Category;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Category;
using FidelizeDotz.Services.Api.Domain.Application.Services.Interfaces;
using FidelizeDotz.Services.Api.Domain.Entities;
using FidelizeDotz.Services.Api.Domain.Enums;
using FidelizeDotz.Services.Api.Domain.Infra.Interfaces;
using static FidelizeDotz.Services.Api.CrossCutting.Constants.ErrorsConstants;
using static FidelizeDotz.Services.Api.Domain.Enums.EReturnMessageType;

namespace FidelizeDotz.Services.Api.Domain.Application.Services
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IAdapter adapter) : base(unitOfWork, adapter)
        {
            
        }

        public async Task<ReturnMessage> InsertCategoryAsyc(InsertCategoryRequest request)
        {
            try
            {
                if (request is null)
                    return new ReturnMessage(RequestIsNull, ClientErrorBadRequest);

                var category = Adapter.ConvertTo<InsertCategoryRequest, Category>(request);
                await UnitOfWork.GetRepository<Category>().InsertAsync(category);
                await UnitOfWork.SaveChangesAsync();

                return new ReturnMessage(true, EReturnMessageType.SuccessCreated);
            }
            catch (Exception)
            {
                return new ReturnMessage(UnexpectedError, ClientErrorBadRequest);
            }
        }

        public async Task<ReturnMessage<IPagedList<CategoryResponse>>> ListCategoriesAsync()
        {
            try
            {
                var listCategories = await UnitOfWork.GetRepository<Category>()
                    .GetPagedListAsync();
                return new ReturnMessage<IPagedList<CategoryResponse>>(
                    Adapter.ConvertTo<PagedList<Category>, PagedList<CategoryResponse>>((PagedList<Category>)listCategories));
            }
            catch (Exception)
            {
                return new ReturnMessage<IPagedList<CategoryResponse>>(UnexpectedError, ClientErrorBadRequest);
            }
        }
    }
}