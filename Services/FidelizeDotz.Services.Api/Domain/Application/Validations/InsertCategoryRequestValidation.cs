using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Category;
using FluentValidation;

namespace FidelizeDotz.Services.Api.Domain.Application.Validations
{
    public class InsertCategoryRequestValidation : AbstractValidator<InsertCategoryRequest>
    {
        public InsertCategoryRequestValidation()
        {
            RuleFor(_ => _.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.Code)
                .NotEmpty()
                .NotNull();
        }
    }
}