using NetBox.Admin.Core.Settings.JobTypes.DTOs;

namespace NetBox.Admin.Core.Settings.JobTypes.Interfaces;

public interface IJobTypeService
{
    Task<ResponseResult<JobTypeDto>> CreateJobType(CreateJobTypeDto model);
    Task<ResponseResult> DeleteJobType(Guid id);
    Task<ResponseResult<IReadOnlyList<JobTypeDto>>> GetAllJobTypes(Paginator paginator, CancellationToken token);
    Task<ResponseResult<JobTypeDto>> GetJobTypeById(Guid id, CancellationToken token);
    Task<ResponseResult> Update(Guid id, UpdateJobTypeDto model);
}
