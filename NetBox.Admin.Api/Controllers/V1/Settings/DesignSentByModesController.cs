using NetBox.Admin.Core.Settings.DesignSentByModes.DTOs;
using NetBox.Admin.Core.Settings.DesignSentByModes.Interfaces;

namespace NetBox.Admin.Api.Controllers.V1.Settings;

[Route("api/settings/design-sent-by-modes")]
public sealed class DesignSentByModesController(IDesignSentByModeService _designSentByModeService) : AppControllerBase
{
    private readonly IDesignSentByModeService _designSentByModeService = _designSentByModeService;

    [HttpPost]
    [Authorize(policy: ApplicationAuthPolicy.Settings.DesignSentByMode.Create)]
    [ProducesResponseType(typeof(ResponseResult<DesignSentByModeDto>), StatusCodes.Status201Created)]
    public async Task<ActionResult> Create([FromForm] CreateDesignSentByModeDto model)
    {
        var response = await _designSentByModeService.Create(model);

        return response.Success ? CreatedAtRoute(nameof(GetDesignSentByById), new { id = response.Data!.Id }, response) :
                                  UnsuccessfulResponse(response);
    }

    [HttpGet("{id}", Name = nameof(GetDesignSentByById))]
    [ProducesResponseType(typeof(ResponseResult<DesignSentByModeDto>), StatusCodes.Status200OK)]
    [Authorize(policy: ApplicationAuthPolicy.Settings.DesignSentByMode.View)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetDesignSentByById([FromRoute] Guid id, CancellationToken token)
    {
        var response = await _designSentByModeService.GetById(id, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpGet]
    [Authorize(policy: ApplicationAuthPolicy.Settings.DesignSentByMode.View)]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<DesignSentByModeDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllDesignSentBy([FromQuery] Paginator paginator, CancellationToken token)
    {
        var response = await _designSentByModeService.GetList(paginator, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpPut("{id}")]
    [Authorize(policy: ApplicationAuthPolicy.Settings.DesignSentByMode.Update)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateDesignSentBy([FromRoute] Guid id, [FromForm] UpdateDesignSentByModeDto model)
    {
        var response = await _designSentByModeService.Update(id, model);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }

    [HttpDelete("{id}")]
    [Authorize(policy: ApplicationAuthPolicy.Settings.DesignSentByMode.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteDesignSentBy([FromRoute] Guid id)
    {
        var response = await _designSentByModeService.Delete(id);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }
}
