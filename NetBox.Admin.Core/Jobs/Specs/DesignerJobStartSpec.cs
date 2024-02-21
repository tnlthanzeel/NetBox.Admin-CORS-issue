namespace NetBox.Admin.Core.Jobs.Specs;

sealed class DesignerJobStartSpec : Specification<Job>
{
    public DesignerJobStartSpec(Guid jobId, string currentAssigneeId)
    {
        Guid assignee = Guid.Parse(currentAssigneeId);

        Query.Where(w => w.CurrentAsigneeId == assignee &&
                         (w.JobStatus != JobStatus.Billed && w.JobStatus != JobStatus.Done))
             .Include(s => s.TimePeriods.OrderByDescending(s => s.CreatedOn))
             .AsTracking();
    }
}
