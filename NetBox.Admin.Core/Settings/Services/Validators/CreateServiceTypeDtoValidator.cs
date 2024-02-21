using NetBox.Admin.Core.Settings.Services.DTOs;

namespace NetBox.Admin.Core.Settings.Services.Validators;

public sealed class CreateServiceTypeDtoValidator : AbstractValidator<CreateServiceTypeDto>
{
    public CreateServiceTypeDtoValidator()
    {
        RuleFor(r => r.Name)
                    .NotEmpty()
                    .WithMessage("Service Type name is requuried")
                    .MaximumLength(AppConstants.StringLengths.FirstName)
                    .WithMessage($"Service Type name must be less than {AppConstants.StringLengths.FirstName} characters");
    }
}
