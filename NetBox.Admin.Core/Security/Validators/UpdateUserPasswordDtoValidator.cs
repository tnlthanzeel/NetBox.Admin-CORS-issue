using FluentValidation;
using NetBox.Admin.Core.Common.Validators;
using NetBox.Admin.Core.Security.Dtos;

namespace NetBox.Admin.Core.Security.Validators;

public sealed class UpdateUserPasswordDtoValidator : AbstractValidator<UpdateUserPasswordDto>
{
    public UpdateUserPasswordDtoValidator()
    {
        RuleFor(r => r.CurrentPassword)
            .NotEmpty().WithMessage("Current Password is required");

        RuleFor(r => r.NewPassword)
            .ValidatePassword(nameof(UpdateUserPasswordDto.NewPassword))
            .Equal(r => r.ConfirmPassword).WithMessage("New Password and Confirm Password does not match");
    }
}
