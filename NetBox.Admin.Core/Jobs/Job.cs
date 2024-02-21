using NetBox.Admin.Core.Customers;
using NetBox.Admin.Core.Security.Entities;
using NetBox.Admin.Core.Settings.ClientTypes;
using NetBox.Admin.Core.Settings.JobTypes;
using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Jobs;

public sealed class Job : EntityBase, ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public Customer? Customer { get; private set; }
    public Guid CustomerId { get; private set; }

    public string PhonNumber { get; private set; } = null!;

    public DateOnly TokenNumberMasterDate { get; private set; }
    public string TokenNumber { get; private set; } = null!;

    public ClientType? ClientType { get; private set; }
    public Guid ClientTypeId { get; private set; }

    public JobType? JobType { get; private set; }
    public Guid JobTypeId { get; private set; }

    public JobStatus JobStatus { get; set; }

    private readonly List<DesignerAssignedJob> _asignees = new();

    public IReadOnlyCollection<DesignerAssignedJob> Asignees => _asignees.AsReadOnly();

    private readonly List<JobTimer> _timePeriods = new();

    public IReadOnlyCollection<JobTimer> TimePeriods => _timePeriods.AsReadOnly();

    public ApplicationUser CurrentAsignee { get; private set; } = null!;
    public Guid CurrentAsigneeId { get; private set; }

    private Job() { }

    public Job(Guid customerId,
               string phoneNumber,
               DateOnly tokenMasterDate,
               string tokenNumber,
               Guid clientTypeId,
               Guid designSentByModeId,
               Guid jobTypeId,
               Guid designerId)
    {
        CustomerId = customerId;
        PhonNumber = phoneNumber;
        TokenNumberMasterDate = tokenMasterDate;
        TokenNumber = tokenNumber;
        ClientTypeId = clientTypeId;
        JobTypeId = jobTypeId;
        JobStatus = JobStatus.Queued;
        CurrentAsigneeId = designerId;
    }

    internal void AssignJob(Guid designerId)
    {
        _asignees.Add(new(designerId));
    }
}
