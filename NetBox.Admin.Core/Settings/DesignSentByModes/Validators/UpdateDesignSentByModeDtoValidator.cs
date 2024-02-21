using NetBox.Admin.Core.Settings.DesignSentByModes.DTOs;

namespace NetBox.Admin.Core.Settings.DesignSentByModes.Validators;

internal sealed class UpdateDesignSentByModeDtoValidator : AbstractValidator<UpdateDesignSentByModeDto>
{
    public UpdateDesignSentByModeDtoValidator()
    {
        RuleFor(r => r.Mode)
         .NotEmpty()
         .WithMessage("Mode is required")
         .MaximumLength(AppConstants.StringLengths.FirstName)
         .WithMessage($"Mode must be less than {AppConstants.StringLengths.FirstName} characters");

        When(r => r.Image is not null,
             () =>
             {
                 RuleFor(r => r.Image)
                 .Cascade(CascadeMode.Stop)
                 .ValidateImageFile();
             });
    }
}
