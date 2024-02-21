using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Settings.Services;

public sealed class Service : EntityBase, ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public ServiceType ServiceType { get; private set; } = null!;
    public Guid ServiceTypeId { get; private set; }

    public string Name { get; private set; } = null!;

    public decimal Rate { get; private set; }

    private Service() { }

    public Service(string name, decimal rate)
    {
        Name = name;
        Rate = rate;
    }

    internal void Deleted()
    {
        IsDeleted = true;
    }

    internal void Update(string name, decimal rate)
    {
        Name = name;
        Rate = rate;
    }
}
