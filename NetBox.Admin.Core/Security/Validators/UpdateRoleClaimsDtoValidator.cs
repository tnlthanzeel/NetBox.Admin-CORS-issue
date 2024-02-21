using NetBox.Admin.Core.Security.Dtos;

namespace NetBox.Admin.Core.Security.Validators;

public sealed class UpdateRoleClaimsDtoValidator : AbstractValidator<UpdateRoleDto>
{
    public UpdateRoleClaimsDtoValidator()
    {
        RuleFor(r => r.Permissions)
                .AppPermissionValueValidation();
    }
}
