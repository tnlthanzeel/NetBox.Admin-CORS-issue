using NetBox.Admin.Core.Settings.Services.DTOs;
using NetBox.Admin.Core.Settings.Services.Interfaces;

namespace NetBox.Admin.Api.Controllers.V1.Settings;

[Route("api/settings/service-types/{serviceTypeId}/services")]
public sealed class ServicesController(IServicesService _servicesService) : AppControllerBase
{
    private readonly IServicesService _servicesService = _servicesService;

    [HttpPost]
    [Authorize(policy: ApplicationAuthPolicy.Settings.Services.Create)]
    [ProducesResponseType(typeof(ResponseResult<ServiceDto>), StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateService([FromRoute] Guid serviceTypeId, [FromBody] CreateServiceDto model)
    {
        var response = await _servicesService.CreateService(serviceTypeId, model);

        return response.Success ? CreatedAtRoute(nameof(GetServiceId),
                                                 new { serviceTypeId = response.Data!.ServiceTypeId, id = response.Data!.Id },
                                                 response) :
                                  UnsuccessfulResponse(response);

    }

    [HttpGet("{id}", Name = nameof(GetServiceId))]
    [ProducesResponseType(typeof(ResponseResult<ServiceDto>), StatusCodes.Status200OK)]
    [Authorize(policy: ApplicationAuthPolicy.Settings.Services.View)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetServiceId([FromRoute] Guid serviceTypeId, [FromRoute] Guid id, CancellationToken token)
    {
        var response = await _servicesService.GetServiceById(serviceTypeId, id, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpDelete("{serviceId}")]
    [Authorize(policy: ApplicationAuthPolicy.Settings.Services.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteService([FromRoute] Guid serviceTypeId, [FromRoute] Guid serviceId)
    {
        var response = await _servicesService.DeleteService(serviceTypeId, serviceId);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }

    [HttpGet]
    [Authorize(policy: ApplicationAuthPolicy.Settings.Services.View)]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<ServiceDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetService([FromRoute] Guid serviceTypeId, [FromQuery] Paginator paginator, CancellationToken token)
    {
        var response = await _servicesService.GetServices(serviceTypeId, paginator, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpPut("{serviceId}")]
    [Authorize(policy: ApplicationAuthPolicy.Settings.Services.Update)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateService([FromRoute] Guid serviceTypeId, [FromRoute] Guid serviceId, [FromBody] UpdateServiceDto model)
    {
        var response = await _servicesService.UpdateService(serviceTypeId, serviceId, model);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }
}
