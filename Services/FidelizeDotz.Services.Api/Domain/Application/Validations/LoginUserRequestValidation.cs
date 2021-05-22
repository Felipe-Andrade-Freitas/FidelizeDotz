using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.User;
using FluentValidation;

namespace FidelizeDotz.Services.Api.Domain.Application.Validations
{
    public class LoginUserRequestValidation : AbstractValidator<LoginUserRequest>
    {
        public LoginUserRequestValidation()
        {
            RuleFor(_ => _.UserName)
                .NotEmpty()
                .NotNull();

            RuleFor(_ => _.Password)
                .NotEmpty()
                .NotNull();
        }   
    }
}