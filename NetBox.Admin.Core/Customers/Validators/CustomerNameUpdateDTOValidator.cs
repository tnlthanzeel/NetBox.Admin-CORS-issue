using NetBox.Admin.Core.Customers.DTOs;

namespace NetBox.Admin.Core.Customers.Validators;

public sealed class CustomerNameUpdateDTOValidator : AbstractValidator<CustomerNameUpdateDTO>
{
    public CustomerNameUpdateDTOValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .WithMessage("Invalid customer ID");

        RuleFor(r => r.Name)
          .MaximumLength(AppConstants.StringLengths.FirstName)
          .WithMessage($"Name must be less than {AppConstants.StringLengths.FirstName} character");
    }
}
