using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Security.Entities;

public sealed class UserProfile : ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FullName => $"{FirstName} {LastName}";

    public string TimeZone { get; set; } = null!;

    public ApplicationUser ApplicationUser { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string? NICNumber { get; set; }
    public string? MobileNumber { get; set; }
}
