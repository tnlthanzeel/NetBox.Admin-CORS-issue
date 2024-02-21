using NetBox.Admin.Core.Jobs.DTOs;
using NetBox.Admin.Core.Jobs.Interfaces;

namespace NetBox.Admin.Api.Controllers.V1.Jobs;

[Route("api/desginer/{designerId}/jobs")]
public sealed class DesignerJobsController : AppControllerBase
{
    private readonly IDesginerAssginedJobManager _desginerAssginedJobManager;

    public DesignerJobsController(IDesginerAssginedJobManager desginerAssginedJobManager)
    {
        _desginerAssginedJobManager = desginerAssginedJobManager;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<DesignerAssignedJobSummaryDTO>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetJobsAssignedToDesigner([FromRoute] Guid designerId, [FromQuery] Paginator paginator, CancellationToken token)
    {
        var response = await _desginerAssginedJobManager.GetJobsAssignedToDesigner(designerId, paginator, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }
}
