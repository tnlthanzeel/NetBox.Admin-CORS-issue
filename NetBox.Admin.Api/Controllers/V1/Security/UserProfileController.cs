using Microsoft.AspNetCore.Mvc;
using NetBox.Admin.Core.Security.Dtos;
using NetBox.Admin.Core.Security.Interfaces;
using NetBox.Admin.SharedKernal.Responses;

namespace NetBox.Admin.Api.Controllers.V1.Security;

[Route("api/security/users/{userId}/profile")]
public sealed class UserProfileController : AppControllerBase
{
    private readonly ISecurityService _securityService;

    public UserProfileController(ISecurityService securityService)
    {
        _securityService = securityService;
    }

    [HttpPut("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ChangeUserPassword([FromRoute] Guid userId, [FromBody] UpdateUserPasswordDto model)
    {
        var response = await _securityService.ChangeUserPassword(userId, model, CancellationToken.None);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseResult<UserProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetUserProfileById([FromRoute] Guid userId, CancellationToken token)
    {
        var response = await _securityService.GetUserProfile(userId, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateUserProfile([FromRoute] Guid userId, [FromBody] UpdateUserProfileDto model)
    {
        var response = await _securityService.UpdateUserProfile(userId, model, CancellationToken.None);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }
}


