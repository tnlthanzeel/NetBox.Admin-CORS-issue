using FluentValidation;
using NetBox.Admin.Core.Security.Dtos;

namespace NetBox.Admin.Core.Security.Validators;

public sealed class AuthenticateUserDtoValidator : AbstractValidator<AuthenticateUserDto>
{
    public AuthenticateUserDtoValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("{PropertyName} is required");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("{PropertyName} is required");
    }
}
