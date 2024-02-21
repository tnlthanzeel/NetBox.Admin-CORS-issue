using FluentValidation;
using NetBox.Admin.Core.Common.Validators;
using NetBox.Admin.Core.Security.Dtos;

namespace NetBox.Admin.Core.Security.Validators;

public sealed class ForgotPasswordModelValidator : AbstractValidator<ForgotPasswordModel>
{
    public ForgotPasswordModelValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required")
            .ValidateEmail();
    }
}
