using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Settings.JobTypes;

public sealed class JobType : EntityBase, ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public string Name { get; private set; } = null!;

    private JobType() { }

    public JobType(string name)
    {
        Name = name;
    }

    internal void Update(string name)
    {
        Name = name;
    }

    internal void Delete()
    {
        IsDeleted = true;
    }
}
