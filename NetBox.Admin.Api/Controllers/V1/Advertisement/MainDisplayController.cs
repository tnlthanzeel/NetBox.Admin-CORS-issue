using NetBox.Admin.Core.Advertisements.DTOs;
using NetBox.Admin.Core.Advertisements.Interfaces;

namespace NetBox.Admin.Api.Controllers.V1.Advertisement;

[Route("api/main-display")]
public sealed class MainDisplayController(IMainDisplayService _mainDisplayService) : AppControllerBase
{
    private readonly IMainDisplayService _mainDisplayService = _mainDisplayService;

    [HttpPost]
    [Authorize(policy: ApplicationAuthPolicy.Advertisment.Create)]
    [ProducesResponseType(typeof(ResponseResult<AdvertisementDto>), StatusCodes.Status201Created)]
    public async Task<ActionResult> Create([FromForm] CreateAdvertismentDto model)
    {
        var response = await _mainDisplayService.Create(model);

        return response.Success ? Created(string.Empty, response) : UnsuccessfulResponse(response);
    }


    [HttpGet]
    [Authorize(policy: ApplicationAuthPolicy.Advertisment.View)]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<AdvertisementDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAll([FromQuery] Paginator paginator, CancellationToken token)
    {
        var response = await _mainDisplayService.GetList(paginator, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }


    [HttpDelete("{id}")]
    [Authorize(policy: ApplicationAuthPolicy.Advertisment.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteDesignSentBy([FromRoute] Guid id)
    {
        var response = await _mainDisplayService.Delete(id);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }
}
