using NetBox.Admin.Core.Jobs.DTOs;
using NetBox.Admin.Core.Jobs.Interfaces;

namespace NetBox.Admin.Api.Controllers.V1.Jobs;

[Route("api/jobs")]
public sealed class JobsController : AppControllerBase
{
    private readonly IJobManager _jobManager;

    public JobsController(IJobManager jobManager)
    {
        _jobManager = jobManager;
    }

    [HttpPost]
    [Authorize(policy: ApplicationAuthPolicy.Job.Create)]
    [ProducesResponseType(typeof(ResponseResult<JobDTO>), StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateJob([FromBody] JobCreateDTO model)
    {
        var response = await _jobManager.CreateJob(model);

        return response.Success ? Created(string.Empty, response) : UnsuccessfulResponse(response);
    }
}
