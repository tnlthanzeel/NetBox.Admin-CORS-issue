using NetBox.Admin.Core.Settings.JobTypes.DTOs;

namespace NetBox.Admin.Core.Settings.JobTypes.Specs;

internal sealed class JobTypeByIdSpec : Specification<JobType, JobTypeDto>
{
    public JobTypeByIdSpec(Guid id)
    {
        Query.Where(w => w.Id == id);

        Query.Select(e => new JobTypeDto(e.Id, e.Name));
    }
}
