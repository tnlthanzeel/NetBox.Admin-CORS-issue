using NetBox.Admin.Core.Security.Dtos;
using NetBox.Admin.Core.Security.Interfaces;

namespace NetBox.Admin.Api.Controllers.V1.Security;

[Route("api/security/roles")]
public sealed class RolesController : AppControllerBase
{
    private readonly IUserRoleService _userRoleService;
    private readonly IUserRolePermissionFacadeService _userRolePermissionFacadeService;

    public RolesController(IUserRoleService userRoleService, IUserRolePermissionFacadeService userRolePermissionFacadeService)
    {
        _userRoleService = userRoleService;
        _userRolePermissionFacadeService = userRolePermissionFacadeService;
    }

    [HttpPost]
    [Authorize(policy: ApplicationAuthPolicy.RolePolicy.Create)]
    [ProducesResponseType(typeof(ResponseResult<UserRoleDto>), StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateRoleAsync([FromBody] UserRoleCreateDto model)
    {
        var response = await _userRoleService.CreateRole(model, CancellationToken.None);

        return response.Success ? CreatedAtRoute(nameof(GetRole), new { id = response.Data!.RoleId }, response) : UnsuccessfulResponse(response);
    }

    [HttpGet]
    [Authorize(policy: ApplicationAuthPolicy.RolePolicy.View)]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<UserRoleDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetRoleList([FromQuery] string? searchQuery, CancellationToken token)
    {
        var response = await _userRoleService.GetAllRoles(searchQuery, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpGet("{id}", Name = "GetRole")]
    [Authorize(policy: ApplicationAuthPolicy.RolePolicy.View)]
    [ProducesResponseType(typeof(ResponseResult<UserRoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetRole([FromRoute] Guid id, CancellationToken token)
    {
        var response = await _userRoleService.GetById(id, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpDelete("{id}")]
    [Authorize(policy: ApplicationAuthPolicy.RolePolicy.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteRole([FromRoute] Guid id)
    {
        var response = await _userRoleService.Delete(id, CancellationToken.None);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }

    [HttpPut("{roleId}")]
    [Authorize(policy: ApplicationAuthPolicy.RolePolicy.UpdateRoleClaim)]
    [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UpdateRole(Guid roleId, [FromBody] UpdateRoleDto model)
    {
        var response = await _userRolePermissionFacadeService.UpdateRole(roleId, model, CancellationToken.None);

        return response.Success ? NoContent() : UnsuccessfulResponse(response);
    }

    [HttpGet("permission-templates")]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<PermissionTemplateDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetRoleClaimTemplates(CancellationToken token)
    {
        var response = await _userRoleService.GetRoleClaimTemplates(token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpGet("{roleId}/permission-templates")]
    [ProducesResponseType(typeof(ResponseResult<PermissionTemplateDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetRoleClaimTemplates(Guid roleId, CancellationToken token)
    {
        var response = await _userRoleService.GetRoleClaimTemplate(roleId, token);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }
}
