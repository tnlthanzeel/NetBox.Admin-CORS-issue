using FluentValidation;
using NetBox.Admin.Core.Common.Validators;
using NetBox.Admin.Core.Security.Dtos;

namespace NetBox.Admin.Core.Security.Validators;

public sealed class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordDtoValidator()
    {
        RuleFor(r => r.Token)
            .NotEmpty().WithMessage("Reset password token is required");

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email address is required");

        RuleFor(r => r.NewPassword)
            .ValidatePassword(nameof(ResetPasswordDto.NewPassword))
             .Equal(r => r.ConfirmPassword).WithMessage("Password and ConfirmPassword does not match");

        RuleFor(r => r.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required");
    }
}
