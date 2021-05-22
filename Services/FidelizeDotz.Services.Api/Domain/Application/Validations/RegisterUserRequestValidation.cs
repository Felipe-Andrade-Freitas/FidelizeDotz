using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.User;
using FluentValidation;

namespace FidelizeDotz.Services.Api.Domain.Application.Validations
{
    public class RegisterUserRequestValidation : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidation()
        {
            RuleFor(_ => _.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(_ => _.PhoneNumber)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(6)
                .MaximumLength(25);

            RuleFor(_ => _.ConfirmPassword)
                .NotEmpty()
                .NotNull()
                .Equal(_ => _.Password);
        }
    }
}