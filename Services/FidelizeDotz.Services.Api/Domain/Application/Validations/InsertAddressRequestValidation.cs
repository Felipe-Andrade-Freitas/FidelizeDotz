using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.User;
using FluentValidation;

namespace FidelizeDotz.Services.Api.Domain.Application.Validations
{
    public class InsertAddressRequestValidation : AbstractValidator<InsertAddressRequest>
    {
        public InsertAddressRequestValidation()
        {
            RuleFor(_ => _.Street)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.Number)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.District)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.City)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.State)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.Country)
                .NotEmpty()
                .NotNull();
        }
    }
}