using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Dotz;
using FluentValidation;

namespace FidelizeDotz.Services.Api.Domain.Application.Validations
{
    public class InsertDotRequestValidation : AbstractValidator<InsertDotRequest>
    {
        public InsertDotRequestValidation()
        {
            RuleFor(_ => _.Quantity)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
        }
    }
}