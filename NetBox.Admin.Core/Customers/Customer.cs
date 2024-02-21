using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Customers;
public sealed class Customer : EntityBase, ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public string PhoneNumber { get; private set; } = null!;
    public string? Name { get; private set; }

    private Customer() { }

    public Customer(string phoneNumber, string? name)
    {
        PhoneNumber = phoneNumber.Trim();
        Name = name?.Trim();
    }

    internal void UpdateName(string? name)
    {
        Name = name?.Trim();
    }
}
