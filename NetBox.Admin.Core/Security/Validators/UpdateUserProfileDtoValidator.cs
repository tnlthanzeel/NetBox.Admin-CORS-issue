using FluentValidation;
using NetBox.Admin.Core.Security.Dtos;
using NetBox.Admin.SharedKernal.Helpers;

namespace NetBox.Admin.Core.Security.Validators;

public sealed class UpdateUserProfileDtoValidator : AbstractValidator<UpdateUserProfileDto>
{
    public UpdateUserProfileDtoValidator()
    {
        RuleFor(f => f.FirstName)
                   .FirstNameValidation();

        RuleFor(f => f.LastName)
                   .LastNameValidation();

        RuleFor(f => f.TimeZone)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("TimeZone is required")
            .Must((model, timeZone) =>
            {
                var isValidTimeZone = TimeZoneHelper.IsTimeZoneAvailable(timeZone);
                return isValidTimeZone;
            }).WithMessage("Invalid Time zone");
    }
}
