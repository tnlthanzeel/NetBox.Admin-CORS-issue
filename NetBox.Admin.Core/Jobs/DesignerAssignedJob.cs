using NetBox.Admin.Core.Jobs.Events;
using NetBox.Admin.Core.Security.Entities;
using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Jobs;
public sealed class DesignerAssignedJob : EntityBase, ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public Job? Job { get; private set; }
    public Guid JobId { get; private set; }

    public ApplicationUser Asignee { get; private set; } = null!;
    public Guid AsigneeId { get; private set; }

    public DesignerJobProgress DesignerJobProgress { get; private set; }

    private DesignerAssignedJob() { }

    public DesignerAssignedJob(Guid designerId)
    {
        AsigneeId = designerId;
        DesignerJobProgress = DesignerJobProgress.NotStarted;

        RegisterDomainEvent(new NewJobAssignedEvent(this));
    }
}
