using NetBox.Admin.Core.Lookups;
using NetBox.Admin.Core.Lookups.DTOs;
using NetBox.Admin.Core.Lookups.Filters;

namespace NetBox.Admin.Api.Controllers;

[Route("api/look-ups")]

public sealed class LookupsController : AppControllerBase
{
    private readonly ILookupService _lookupService;

    public LookupsController(ILookupService lookupService)
    {
        _lookupService = lookupService;
    }

    [HttpGet("client-types")]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<ClientTypeLookupDTO>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetClientTypes(CancellationToken token)
    {
        var response = await _lookupService.GetClientTypesForLookup(token);
        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpGet("job-types")]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<JobTypeLookupDTO>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetJobTypes(CancellationToken token)
    {
        var response = await _lookupService.GetJobTypesForLookup(token);
        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpGet("design-sentby-modes")]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<DesignSentByModeLookupDTO>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetDesignSentByModes(CancellationToken token)
    {
        var response = await _lookupService.GetDesignSentByModes(token);
        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpGet("customers")]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<CustomerLookupDTO>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetCustomers([FromQuery] CustomerLookupFilter filter, CancellationToken token)
    {
        var response = await _lookupService.GetCustomersForLookup(filter, token);
        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpGet("users")]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<UserLookupDTO>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetUsers(CancellationToken token)
    {
        var response = await _lookupService.GetUsersForLookup(token);
        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }
}
