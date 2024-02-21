using NetBox.Admin.Core.Jobs.DTOs;

namespace NetBox.Admin.Core.Jobs.Validators;

public sealed class JobCreateDTOValidator : AbstractValidator<JobCreateDTO>
{
    public JobCreateDTOValidator()
    {
        When(w => w.CustomerId.HasValue,
            () =>
            {
                RuleFor(r => r.PhoneNumber)
                .Null()
                .WithMessage("Cannot have customer Id and phone number");

                RuleFor(r => r.CustomerName)
                 .MaximumLength(AppConstants.StringLengths.FirstName)
                 .WithMessage($"Name must be less than {AppConstants.StringLengths.FirstName} character");
            });

        When(w => w.CustomerId is null,
          () =>
          {
              RuleFor(r => r.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required")
                .MinimumLength(9)
                .WithMessage("Phone number must be atleast 9 numbers")
                .MaximumLength(20)
                .WithMessage("Phone number must be less than 20 numbers");

              RuleFor(r => r.CustomerName)
                  .MaximumLength(AppConstants.StringLengths.FirstName)
                  .WithMessage($"Name must be less than {AppConstants.StringLengths.FirstName} character");
          });

        RuleFor(r => r.DesignerId)
            .NotEmpty()
            .WithMessage("Designer is required");

        RuleFor(r => r.ClientType)
           .NotEmpty()
           .WithMessage("Client type is required");

        RuleFor(r => r.JobType)
           .NotEmpty()
           .WithMessage("Job type is required");

        RuleFor(r => r.DesignSentByModeId)
           .NotEmpty()
           .WithMessage("Design a=sent by mode is required");

    }
}
