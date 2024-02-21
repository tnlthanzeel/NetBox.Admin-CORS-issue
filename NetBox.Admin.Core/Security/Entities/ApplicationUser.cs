using Microsoft.AspNetCore.Identity;
using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Security.Entities;

public sealed class ApplicationUser : IdentityUser<Guid>, IAggregateRoot, ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public UserProfile UserProfile { get; set; } = null!;

    public void Deleted()
    {
        IsDeleted = true;
    }

    public void UpdateProfileInfoByAdmin(string firstName, string lastName, string displayName, string? nic, string? mobile)
    {
        UserProfile.FirstName = firstName;
        UserProfile.LastName = lastName;
        UserProfile.DisplayName = displayName;
        UserProfile.NICNumber = nic;
        UserProfile.MobileNumber = mobile;
    }

    public void UpdateProfileInfoUser(string firstName, string lastName)
    {
        UserProfile.FirstName = firstName;
        UserProfile.LastName = lastName;
    }

    public void UpdateTimeZone(string timeZone)
    {
        UserProfile.TimeZone = timeZone;
    }
}
