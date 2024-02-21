using Microsoft.AspNetCore.Identity;

namespace NetBox.Admin.Core.Security.Entities;

public sealed class UserClaim : IdentityUserClaim<Guid>
{
    public int? UserRoleClaimId { get; set; }
    public Guid? UserRoleId { get; set; }
}
