using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Dotz;
using FluentValidation;

namespace FidelizeDotz.Services.Api.Domain.Application.Validations
{
    public class RescuedDotRequestValidation : AbstractValidator<RescuedDotRequest>
    {
        public RescuedDotRequestValidation()
        {
            RuleFor(_ => _.Quantity)
                .LessThan(0)
                .NotNull()
                .NotEmpty();
        }
    }
}