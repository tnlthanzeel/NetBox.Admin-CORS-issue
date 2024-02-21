using NetBox.Admin.Core.Settings.Services.DTOs;

namespace NetBox.Admin.Core.Settings.Services.Validators;

public sealed class UpdateServiceTypeDtoValidator : AbstractValidator<UpdateServiceTypeDto>
{
    public UpdateServiceTypeDtoValidator()
    {
        RuleFor(r => r.Name)
          .NotEmpty()
          .WithMessage("Service Type name is required")
          .MaximumLength(AppConstants.StringLengths.FirstName)
          .WithMessage($"Service Type name must be less than {AppConstants.StringLengths.FirstName} characters");
    }
}
