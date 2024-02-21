using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBox.Admin.Core.Security.AuthPolicies;
using NetBox.Admin.Core.Security.Dtos;
using NetBox.Admin.Core.Security.Filters;
using NetBox.Admin.Core.Security.Interfaces;
using NetBox.Admin.SharedKernal.Models;
using NetBox.Admin.SharedKernal.Responses;

namespace NetBox.Admin.Api.Controllers.V1.Security;

[Route("api/security/users")]
public sealed class UsersController : AppControllerBase
{
    private readonly ISecurityService _securityService;

    public UsersController(ISecurityService securityService)
    {
        _securityService = securityService;
    }

    [HttpPost]
    [Authorize(policy: ApplicationAuthPolicy.UserPolicy.Create)]
    [ProducesResponseType(typeof(ResponseResult<UserDto>), StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateUser([FromBody] UserCreateDto model)
    {
        var response = await _securityService.CreateUser(model);

        return response.Success ? CreatedAtRoute(nameof(GetUserById), new { id = response.Data!.Id }, response) : UnsuccessfulResponse(response);
    }

    /// <summary>
    /// Get a user by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetUserById")]
    [Authorize(policy: ApplicationAuthPolicy.UserPolicy.View)]
    [ProducesResponseType(typeof(ResponseResult<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetUserById([FromRoute] Guid id, CancellationToken token)
    {
        var response = await _securityService.GetUser(id, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpGet]
    [Authorize(policy: ApplicationAuthPolicy.UserPolicy.View)]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<UserSummaryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetUserList([FromQuery] Paginator paginator, [FromQuery] UserFilter filter, CancellationToken token)
    {
        var response = await _securityService.GetList(paginator, filter, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpPut("{id}")]
    [Authorize(policy: ApplicationAuthPolicy.UserPolicy.Edit)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDto model)
    {
        var response = await _securityService.UpdateUser(id, model, CancellationToken.None);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }

    [HttpDelete("{id}")]
    [Authorize(policy: ApplicationAuthPolicy.UserPolicy.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteUser([FromRoute] Guid id)
    {
        var response = await _securityService.DeleteUser(id, CancellationToken.None);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }
}
