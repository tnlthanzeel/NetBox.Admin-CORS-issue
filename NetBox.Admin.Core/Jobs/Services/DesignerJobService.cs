using NetBox.Admin.Core.Jobs.DTOs;
using NetBox.Admin.Core.Jobs.Interfaces;
using NetBox.Admin.Core.Jobs.Specs;
using NetBox.Admin.SharedKernal.Interfaces;

namespace NetBox.Admin.Core.Jobs.Services;

sealed class DesignerJobService : IDesignerJobService
{
    private readonly IJobRepository _jobRepository;
    private readonly ILoggedInUserService _loggedInUser;

    public DesignerJobService(IJobRepository jobRepository, ILoggedInUserService loggedInUser)
    {
        _jobRepository = jobRepository;
        _loggedInUser = loggedInUser;
    }

    public async Task<ResponseResult> StartJob(Guid jobId)
    {
        var job = await _jobRepository.GetBySpec(new DesignerJobStartSpec(jobId, _loggedInUser.UserId));

        if (job is null) return new(new NotFoundException(nameof(jobId), "Job", jobId));



        return new();
    }

    ResponseResult<JobDTO> IDesignerJobService.CreateJob(CreateJobInternalDTO newJob)
    {
        Job job = new(newJob.CustomerId,
                      newJob.PhoneNumber,
                      newJob.TokenMasterDate,
                      newJob.TokenNumber,
                      newJob.ClientTypeId,
                      newJob.DesignSentByModeId,
                      newJob.JobTypeId,
                      newJob.DesignerId);

        job.AssignJob(newJob.DesignerId);

        _jobRepository.Add(job);

        JobDTO jobDto = new(job.Id,
                            newJob.DesignerId,
                            newJob.TokenNumber,
                            newJob.TokenMasterDate);

        return new(jobDto);
    }
}
