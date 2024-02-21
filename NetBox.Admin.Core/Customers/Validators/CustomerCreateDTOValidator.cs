using NetBox.Admin.Core.Customers.DTOs;

namespace NetBox.Admin.Core.Customers.Validators;

public sealed class CustomerCreateDTOValidator : AbstractValidator<CustomerCreateDTO>
{
    public CustomerCreateDTOValidator()
    {
        RuleFor(r => r.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required")
            .MinimumLength(9)
            .WithMessage("Phone number must be atleast 9 numbers")
            .MaximumLength(20)
            .WithMessage("Phone number must be less than 20 numbers");

        RuleFor(r => r.Name)
            .MaximumLength(AppConstants.StringLengths.FirstName)
            .WithMessage($"Name must be less than {AppConstants.StringLengths.FirstName} character");
    }
}
