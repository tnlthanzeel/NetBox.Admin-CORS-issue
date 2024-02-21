using NetBox.Admin.Core.Security.Entities;
using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Jobs;

public sealed class JobTimer : EntityBase, ICreatedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }

    public Job? Job { get; private set; }
    public Guid JobId { get; private set; }

    public ApplicationUser? Designer { get; private set; }
    public Guid DesignerId { get; private set; }

    public int CumulatedMinutes { get; private set; }

    public JobTimerStatus Status { get; set; }

    private JobTimer() { }
}
