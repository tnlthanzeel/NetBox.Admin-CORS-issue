using NetBox.Admin.Core.Settings.JobTypes.DTOs;

namespace NetBox.Admin.Core.Settings.JobTypes.Validators;

public sealed class UpdateJobTypeDtoValidator:AbstractValidator<UpdateJobTypeDto>
{
    public UpdateJobTypeDtoValidator()
    {
        RuleFor(r => r.Name)
          .NotEmpty()
          .WithMessage("Job Type is required")
          .MaximumLength(AppConstants.StringLengths.FirstName)
          .WithMessage($"Job Type must be less than {AppConstants.StringLengths.FirstName} characters");
    }
}
