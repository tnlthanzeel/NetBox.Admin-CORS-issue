using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Advertisements;

public sealed class Advertisement : EntityBase, ICreatedAudit, IDeletedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public string FilelURL { get; private set; } = null!;

    public bool IsImage { get; private set; }

    private Advertisement() { }

    public Advertisement(string fileURL, bool isImage)
    {
        FilelURL = fileURL;
        IsImage = isImage;
    }

    internal void Delete() => IsDeleted = true;
}
