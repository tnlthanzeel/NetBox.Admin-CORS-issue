using NetBox.Admin.Core.Jobs.DTOs;

namespace NetBox.Admin.Core.Jobs.Interfaces;

public interface IDesignerJobService
{
    Task<ResponseResult> StartJob(Guid jobId);
    internal ResponseResult<JobDTO> CreateJob(CreateJobInternalDTO newJob);
}
