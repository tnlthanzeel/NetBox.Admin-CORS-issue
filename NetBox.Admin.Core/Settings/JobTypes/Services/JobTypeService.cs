using NetBox.Admin.Core.Settings.JobTypes.DTOs;
using NetBox.Admin.Core.Settings.JobTypes.Interfaces;
using NetBox.Admin.Core.Settings.JobTypes.Specs;
using NetBox.Admin.Core.Settings.JobTypes.Validators;

namespace NetBox.Admin.Core.Settings.JobTypes.Services;

internal sealed class JobTypeService(IJobTypeRepository _jobTypeRepository,
                                     IModelValidator _validator) : IJobTypeService
{
    private readonly IJobTypeRepository _jobTypeRepository = _jobTypeRepository;
    private readonly IModelValidator _validator = _validator;

    public async Task<ResponseResult<JobTypeDto>> CreateJobType(CreateJobTypeDto model)
    {
        var validationResult = await _validator.ValidateAsync<CreateJobTypeDtoValidator, CreateJobTypeDto>(model);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        JobType jobType = new(model.Name);

        _jobTypeRepository.Add(jobType);

        await _jobTypeRepository.SaveChangesAsync();

        JobTypeDto jobTypeDto = new(jobType.Id, jobType.Name);
        return new(jobTypeDto);
    }

    public async Task<ResponseResult> DeleteJobType(Guid id)
    {
        var jobType = await _jobTypeRepository.GetBySpec(new JobTypeDeleteSpec(id));

        if (jobType is null) return new(new NotFoundException(nameof(id), "Job Type", id));

        // TODO: validate if any job type in use before deleting
        jobType.Delete();

        await _jobTypeRepository.SaveChangesAsync();

        return new();
    }

    public async Task<ResponseResult<IReadOnlyList<JobTypeDto>>> GetAllJobTypes(Paginator paginator, CancellationToken token)
    {
        var (list, totalRecords) = await _jobTypeRepository.GetProjectedListBySpec(paginator, new JobTypeListSpec(), token);

        return new(list, totalRecords);
    }

    public async Task<ResponseResult<JobTypeDto>> GetJobTypeById(Guid id, CancellationToken token)
    {
        var jobTypeDto = await _jobTypeRepository.GetProjectedEntityBySpec(new JobTypeByIdSpec(id), token);

        if (jobTypeDto is null) return new(new NotFoundException(nameof(id), "Job Type", id));

        return new(jobTypeDto);
    }

    public async Task<ResponseResult> Update(Guid id, UpdateJobTypeDto model)
    {
        var validationResult = await _validator.ValidateAsync<UpdateJobTypeDtoValidator, UpdateJobTypeDto>(model);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        var jobType = await _jobTypeRepository.GetBySpec(new JobTypeUpdateSpec(id));

        if (jobType is null) return new(new NotFoundException(nameof(id), "Job Type", id));

        jobType.Update(model.Name);

        await _jobTypeRepository.SaveChangesAsync();

        return new();
    }
}
