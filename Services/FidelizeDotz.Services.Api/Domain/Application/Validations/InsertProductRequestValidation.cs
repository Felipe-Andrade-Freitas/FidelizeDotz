using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Product;
using FluentValidation;

namespace FidelizeDotz.Services.Api.Domain.Application.Validations
{
    public class InsertProductRequestValidation : AbstractValidator<InsertProductRequest>
    {
        public InsertProductRequestValidation()
        {
            RuleFor(_ => _.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.Price)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.PriceDots)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.ImageUrl)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.SkuCode)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.CategoryId)
                .NotEmpty()
                .NotNull();
        }
    }
}