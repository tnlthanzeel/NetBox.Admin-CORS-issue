using NetBox.Admin.Core.Settings.ClientTypes.DTOs;
using NetBox.Admin.Core.Settings.ClientTypes.Interfaces;

namespace NetBox.Admin.Api.Controllers.V1.Settings;

[Route("api/settings/client-types")]
public sealed class ClientTypesController(IClientTypeService _clientTypeService) : AppControllerBase
{
    private readonly IClientTypeService _clientTypeService = _clientTypeService;

    [HttpPost]
    [Authorize(policy: ApplicationAuthPolicy.Settings.ClientType.Create)]
    [ProducesResponseType(typeof(ResponseResult<ClientTypeDto>), StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateService([FromBody] CreateClientTypeDto model)
    {
        var response = await _clientTypeService.CreateClientType(model);

        return response.Success ? CreatedAtRoute(nameof(GetClientTypeById), new { id = response.Data!.Id }, response) :
                                  UnsuccessfulResponse(response);
    }

    [HttpGet("{id}", Name = nameof(GetClientTypeById))]
    [ProducesResponseType(typeof(ResponseResult<ClientTypeDto>), StatusCodes.Status200OK)]
    [Authorize(policy: ApplicationAuthPolicy.Settings.ClientType.View)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetClientTypeById([FromRoute] Guid id, CancellationToken token)
    {
        var response = await _clientTypeService.GetClientTypeById(id, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpDelete("{id}")]
    [Authorize(policy: ApplicationAuthPolicy.Settings.ClientType.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteClientType([FromRoute] Guid id)
    {
        var response = await _clientTypeService.DeleteClientType(id);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }

    [HttpGet]
    [Authorize(policy: ApplicationAuthPolicy.Settings.ClientType.View)]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<ClientTypeDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllClientTypes([FromQuery] Paginator paginator, CancellationToken token)
    {
        var response = await _clientTypeService.GetAllClientTypes(paginator, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpPut("{id}")]
    [Authorize(policy: ApplicationAuthPolicy.Settings.ClientType.Update)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateClientType([FromRoute] Guid id, [FromBody] UpdateClientTypeDto model)
    {
        var response = await _clientTypeService.Update(id, model);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }
}
