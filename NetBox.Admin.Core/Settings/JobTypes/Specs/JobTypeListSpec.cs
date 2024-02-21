using NetBox.Admin.Core.Settings.JobTypes.DTOs;

namespace NetBox.Admin.Core.Settings.JobTypes.Specs;
internal sealed class JobTypeListSpec : Specification<JobType, JobTypeDto>
{
    public JobTypeListSpec()
    {
        Query.OrderBy(w => w.Name);

        Query.Select(e => new JobTypeDto(e.Id, e.Name));
    }
}
