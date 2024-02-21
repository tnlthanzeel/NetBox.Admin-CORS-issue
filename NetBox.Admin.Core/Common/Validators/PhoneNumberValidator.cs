using FluentValidation;
using NetBox.Admin.SharedKernal;

namespace NetBox.Admin.Core.Common.Validators;

public static class PhoneNumberValidator
{
    public static IRuleBuilderOptions<T, string> ValidatePhoneNumber<T>(this IRuleBuilder<T, string?> rule)
    {
        return rule
            .MinimumLength(10).WithMessage("Must be at least 10 characters")
            .MaximumLength(AppConstants.StringLengths.PhoneNumber).WithMessage($"Must be less than {AppConstants.StringLengths.PhoneNumber} characters");
    }
}
