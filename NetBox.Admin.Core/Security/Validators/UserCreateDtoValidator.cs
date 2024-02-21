using NetBox.Admin.Core.Security.Dtos;

namespace NetBox.Admin.Core.Security.Validators;

public sealed class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateDtoValidator(IUserSecurityRespository userSecurityRespository)
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required")
            .ValidateEmail();

        RuleFor(r => r.UserName)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(256).WithMessage("Username must be less than 256 characters");

        RuleFor(r => r.Password)
            .ValidatePassword(nameof(UserCreateDto.Password))
            .Equal(r => r.ConfirmPassword).WithMessage("Password and ConfirmPassword does not match");

        RuleFor(r => r.ConfirmPassword)
            .NotEmpty();

        RuleFor(r => r.FirstName)
             .FirstNameValidation();

        RuleFor(r => r.LastName)
            .LastNameValidation();

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

        RuleFor(r => r.DisplayName)
           .NotEmpty()
           .WithMessage("Display Name is required")
           .MaximumLength(AppConstants.StringLengths.FirstName)
           .WithMessage($"Display Name must be less than {AppConstants.StringLengths.FirstName} characters");

        RuleFor(r => r.NICNumber)
            .MaximumLength(AppConstants.StringLengths.PhoneNumber)
            .WithMessage($"NIC number must be less than {AppConstants.StringLengths.PhoneNumber} characters");

        RuleFor(r => r.MobileNumber)
            .MaximumLength(AppConstants.StringLengths.PhoneNumber)
            .WithMessage($"Mobile number must be less than {AppConstants.StringLengths.PhoneNumber} characters");

    }
}
