using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Settings.ClientTypes;

public sealed class ClientType : EntityBase, ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public string ClientTypeValue { get; private set; } = null!;

    private ClientType() { }

    public ClientType(string clientType)
    {
        ClientTypeValue = clientType;
    }

    internal void Delete()
    {
        IsDeleted = true;
    }

    internal void Update(string clientType)
    {
        ClientTypeValue = clientType;
    }
}
