using NetBox.Admin.Core.Lookups.DTOs;
using NetBox.Admin.Core.Settings.JobTypes;

namespace NetBox.Admin.Core.Lookups.Specs;

sealed class JobTypeLookupSpec : Specification<JobType, JobTypeLookupDTO>
{
    public JobTypeLookupSpec()
    {
        Query.OrderBy(o => o.Name);

        Query.Select(s => new JobTypeLookupDTO(s.Id, s.Name));
    }
}
