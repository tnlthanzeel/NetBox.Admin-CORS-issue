using NetBox.Admin.Core.Settings.Services.DTOs;

namespace NetBox.Admin.Core.Settings.Services.Validators;

public sealed class UpdateServiceDtoValidator : AbstractValidator<UpdateServiceDto>
{
    public UpdateServiceDtoValidator()
    {
        RuleFor(r => r.Name)
          .NotEmpty()
          .WithMessage("Service name is required")
          .MaximumLength(AppConstants.StringLengths.FirstName)
          .WithMessage($"Service name must be less than {AppConstants.StringLengths.FirstName} characters");

        RuleFor(r => r.Rate)
          .GreaterThanOrEqualTo(0)
          .WithMessage($"Service rate must be zero or greater");
    }
}
