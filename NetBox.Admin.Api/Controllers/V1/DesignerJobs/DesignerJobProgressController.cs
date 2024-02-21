using NetBox.Admin.Core.Jobs.Interfaces;

namespace NetBox.Admin.Api.Controllers.V1.DesignerJobs;

[Route("api/jobs/{jobId}/jobstatus")]
public sealed class DesignerJobProgressController : AppControllerBase
{
    private readonly IDesignerJobService _designerJobService;

    public DesignerJobProgressController(IDesignerJobService designerJobService)
    {
        _designerJobService = designerJobService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> StartJob([FromRoute] Guid jobId)
    {
        var response = await _designerJobService.StartJob(jobId);

        return response.Success ? Ok() : UnsuccessfulResponse(response);
    }
}
