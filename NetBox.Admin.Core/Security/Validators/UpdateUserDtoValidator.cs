using FluentValidation;
using NetBox.Admin.Core.Common.Validators;
using NetBox.Admin.Core.Security.Dtos;
using NetBox.Admin.Core.Security.Interfaces;
using NetBox.Admin.SharedKernal.Helpers;

namespace NetBox.Admin.Core.Security.Validators;

public sealed class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator(IUserSecurityRespository userSecurityRespository)
    {
        RuleFor(f => f.FirstName)
                   .FirstNameValidation();

        RuleFor(f => f.LastName)
                   .LastNameValidation();

        RuleFor(f => f.Email)
            .NotEmpty().WithMessage("Email is required")
            .ValidateEmail();

        RuleFor(r => r.Role)
           .NotEmpty().WithMessage("Role is required");

        RuleFor(r => r.Permissions)
            .AppPermissionValueValidation();

        RuleFor(r => r.Role)
         .AppRoleValidation(userSecurityRespository);

        RuleFor(f => f.TimeZone)
          .Cascade(CascadeMode.Stop)
          .NotEmpty().WithMessage("TimeZone is required")
          .Must((model, timeZone) =>
          {
              var isValidTimeZone = TimeZoneHelper.IsTimeZoneAvailable(timeZone);
              return isValidTimeZone;
          }).WithMessage("Invalid Time zone");

        RuleFor(r => r.NICNumber)
         .MaximumLength(AppConstants.StringLengths.PhoneNumber)
         .WithMessage($"NIC number must be less than {AppConstants.StringLengths.PhoneNumber} characters");

        RuleFor(r => r.MobileNumber)
            .MaximumLength(AppConstants.StringLengths.PhoneNumber)
            .WithMessage($"Mobile number must be less than {AppConstants.StringLengths.PhoneNumber} characters");

        RuleFor(r => r.DisplayName)
         .NotEmpty()
         .WithMessage("Display Name is required")
         .MaximumLength(AppConstants.StringLengths.FirstName)
         .WithMessage($"Display Name must be less than {AppConstants.StringLengths.FirstName} characters");
    }
}
