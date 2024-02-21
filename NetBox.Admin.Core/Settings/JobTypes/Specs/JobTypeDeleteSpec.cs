namespace NetBox.Admin.Core.Settings.JobTypes.Specs;

internal sealed class JobTypeDeleteSpec : Specification<JobType>
{
    public JobTypeDeleteSpec(Guid id)
    {
        Query.Where(w => w.Id == id)
             .AsTracking();
    }
}
