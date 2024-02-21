using FluentValidation;
using NetBox.Admin.SharedKernal;

namespace NetBox.Admin.Core.Security.Validators;

public static class UserLastNameValidator
{
    public static IRuleBuilderOptions<T, string> LastNameValidation<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
                .NotEmpty().WithMessage("LastName is required")
            .MaximumLength(AppConstants.StringLengths.LastName).WithMessage($"LastName must be less than {AppConstants.StringLengths.LastName} characters");
    }
}
