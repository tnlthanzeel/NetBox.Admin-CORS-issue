using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Settings.DesignSentByModes;

public sealed class DesignSentByMode : EntityBase, ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public string Mode { get; private set; } = null!;
    public string ImageURL { get; private set; } = null!;

    private DesignSentByMode() { }

    public DesignSentByMode(string mode, string imageURL)
    {
        Mode = mode;
        ImageURL = imageURL;
    }

    internal void Update(string mode, string? url)
    {
        Mode = mode;
        ImageURL = url ?? ImageURL;
    }

    internal void Delete()
    {
        IsDeleted = true;
    }
}
