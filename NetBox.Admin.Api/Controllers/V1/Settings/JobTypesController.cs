using NetBox.Admin.Core.Settings.JobTypes.DTOs;
using NetBox.Admin.Core.Settings.JobTypes.Interfaces;

namespace NetBox.Admin.Api.Controllers.V1.Settings;

[Route("api/settings/job-types")]
public sealed class JobTypesController(IJobTypeService _jobTypeService) : AppControllerBase
{
    private readonly IJobTypeService _jobTypeService = _jobTypeService;

    [HttpPost]
    [Authorize(policy: ApplicationAuthPolicy.Settings.JobType.Create)]
    [ProducesResponseType(typeof(ResponseResult<JobTypeDto>), StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateJobType([FromBody] CreateJobTypeDto model)
    {
        var response = await _jobTypeService.CreateJobType(model);

        return response.Success ? CreatedAtRoute(nameof(GetJobTypeById), new { id = response.Data!.Id }, response) :
                                  UnsuccessfulResponse(response);

    }

    [HttpGet("{id}", Name = nameof(GetJobTypeById))]
    [ProducesResponseType(typeof(ResponseResult<JobTypeDto>), StatusCodes.Status200OK)]
    [Authorize(policy: ApplicationAuthPolicy.Settings.JobType.View)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetJobTypeById([FromRoute] Guid id, CancellationToken token)
    {
        var response = await _jobTypeService.GetJobTypeById(id, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpGet]
    [Authorize(policy: ApplicationAuthPolicy.Settings.JobType.View)]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<JobTypeDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllJobTypes([FromQuery] Paginator paginator, CancellationToken token)
    {
        var response = await _jobTypeService.GetAllJobTypes(paginator, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpPut("{id}")]
    [Authorize(policy: ApplicationAuthPolicy.Settings.JobType.Update)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateJobType([FromRoute] Guid id, [FromBody] UpdateJobTypeDto model)
    {
        var response = await _jobTypeService.Update(id, model);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }

    [HttpDelete("{id}")]
    [Authorize(policy: ApplicationAuthPolicy.Settings.JobType.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteJobType([FromRoute] Guid id)
    {
        var response = await _jobTypeService.DeleteJobType(id);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }
}
