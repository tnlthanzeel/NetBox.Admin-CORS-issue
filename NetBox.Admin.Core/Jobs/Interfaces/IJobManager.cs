using NetBox.Admin.Core.Jobs.DTOs;

namespace NetBox.Admin.Core.Jobs.Interfaces;

public interface IJobManager
{
    Task<ResponseResult<JobDTO>> CreateJob(JobCreateDTO model);
}
