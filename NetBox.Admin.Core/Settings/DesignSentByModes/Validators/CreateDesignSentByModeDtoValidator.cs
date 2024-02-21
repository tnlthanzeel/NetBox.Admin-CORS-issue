using NetBox.Admin.Core.Settings.DesignSentByModes.DTOs;

namespace NetBox.Admin.Core.Settings.DesignSentByModes.Validators;

public sealed class CreateDesignSentByModeDtoValidator:AbstractValidator<CreateDesignSentByModeDto>
{
    public CreateDesignSentByModeDtoValidator()
    {
        RuleFor(r => r.Mode)
           .NotEmpty()
           .WithMessage("Mode is required")
           .MaximumLength(AppConstants.StringLengths.FirstName)
           .WithMessage($"Mode must be less than {AppConstants.StringLengths.FirstName} characters");

        RuleFor(r => r.Image)
            .Cascade(CascadeMode.Stop)
            .ValidateImageFile();
    }
}
