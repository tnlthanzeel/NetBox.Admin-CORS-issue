namespace NetBox.Admin.Core.Settings.JobTypes.Specs;

internal sealed class JobTypeUpdateSpec : Specification<JobType>
{
    public JobTypeUpdateSpec(Guid id)
    {
        Query.Where(w => w.Id == id)
             .AsTracking();

    }
}
