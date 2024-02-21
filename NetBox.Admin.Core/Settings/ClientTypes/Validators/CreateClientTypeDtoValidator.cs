using NetBox.Admin.Core.Settings.ClientTypes.DTOs;

namespace NetBox.Admin.Core.Settings.ClientTypes.Validators;

public sealed class CreateClientTypeDtoValidator : AbstractValidator<CreateClientTypeDto>
{
    public CreateClientTypeDtoValidator()
    {
        RuleFor(r => r.ClientType)
            .NotEmpty()
            .WithMessage("Client Type is required")
            .MaximumLength(AppConstants.StringLengths.FirstName)
            .WithMessage($"Client Type must be less than {AppConstants.StringLengths.FirstName} characters");
    }
}
