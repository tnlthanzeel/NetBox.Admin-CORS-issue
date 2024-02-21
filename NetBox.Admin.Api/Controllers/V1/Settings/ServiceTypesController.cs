using NetBox.Admin.Core.Settings.Services.DTOs;
using NetBox.Admin.Core.Settings.Services.Filters;
using NetBox.Admin.Core.Settings.Services.Interfaces;

namespace NetBox.Admin.Api.Controllers.V1.Settings;

[Route("api/settings/service-types")]
public sealed class ServiceTypesController : AppControllerBase
{
    private readonly IServiceTypeService _serviceTypeService;

    public ServiceTypesController(IServiceTypeService serviceTypeService)
    {
        _serviceTypeService = serviceTypeService;
    }

    [HttpPost]
    [Authorize(policy: ApplicationAuthPolicy.Settings.Services.Create)]
    [ProducesResponseType(typeof(ResponseResult<ServiceTypeSummaryDto>), StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateServiceType([FromBody] CreateServiceTypeDto model)
    {
        var response = await _serviceTypeService.CreateServiceType(model);

        return response.Success ? CreatedAtRoute(nameof(GetServiceTypeId), new { id = response.Data!.Id }, response) : UnsuccessfulResponse(response);

    }

    [HttpGet("{id}", Name = nameof(GetServiceTypeId))]
    [ProducesResponseType(typeof(ResponseResult<ServiceTypeSummaryDto>), StatusCodes.Status200OK)]
    [Authorize(policy: ApplicationAuthPolicy.Settings.Services.View)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetServiceTypeId([FromRoute] Guid id, CancellationToken token)
    {
        var response = await _serviceTypeService.GetServiceTypeId(id, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpDelete("{serviceTypeId}")]
    [Authorize(policy: ApplicationAuthPolicy.Settings.Services.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteServiceType([FromRoute] Guid serviceTypeId)
    {
        var response = await _serviceTypeService.DeleteServiceType(serviceTypeId);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }

    [HttpGet]
    [Authorize(Roles ="NoEntry")]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<ServiceTypeDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetServiceTypes([FromQuery] Paginator paginator, [FromQuery] ServiceTypeFilter filter, CancellationToken token)
    {
        var response = await _serviceTypeService.GetServiceTypes(paginator, filter, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpPut("{serviceTypeId}")]
    [Authorize(policy: ApplicationAuthPolicy.Settings.Services.Update)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateServiceType([FromRoute] Guid serviceTypeId, [FromBody] UpdateServiceTypeDto model)
    {
        var response = await _serviceTypeService.Update(serviceTypeId, model);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }

}
