using NetBox.Admin.Core.Security.Dtos;
using NetBox.Admin.Core.Security.Interfaces;
using NetBox.Admin.Core.Security.ModulePermissions;

namespace NetBox.Admin.Api.Controllers.V1.Security;

[Route("api/security")]
public sealed class SecurityController : AppControllerBase
{
    private readonly ISecurityService _securityService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public SecurityController(ISecurityService securityService, IWebHostEnvironment webHostEnvironment)
    {
        _securityService = securityService;
        _webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// Authenticate a user by providing the username and password
    /// </summary>
    /// <param name="model"></param>
    /// <returns>A Respoonse result object conataining the authenticated user info</returns>
    [AllowAnonymous]
    [HttpPost("authenticate")]
    [ProducesResponseType(typeof(ResponseResult<AuthenticatedUserDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Authenticate([FromBody] AuthenticateUserDto model)
    {
        var response = await _securityService.AuthenticateUser(model, CancellationToken.None);

        return response.Success ? Ok(response) : UnsuccessfulResponse(response);
    }

    [HttpGet("app-permissions")]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<KeyValuePair<string, List<KeyValuePair<string, IReadOnlyList<PermissionSet>>>>>>), StatusCodes.Status200OK)]
    public ActionResult GeApplicationPermissions()
    {
        var permissionList = AppModulePermissions.GetPermissionList();

        var response = new ResponseResult<IReadOnlyList<KeyValuePair<string, List<KeyValuePair<string, IReadOnlyList<PermissionSet>>>>>>
                                          (permissionList, permissionList.Count);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> SendPasswordResetEmail([FromBody] ForgotPasswordModel forgotPasswordModel)
    {
        var response = await _securityService.SendResetPasswordEmail(forgotPasswordModel, CancellationToken.None);

        return response.Success ? Ok() : UnsuccessfulResponse(response);
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    {
        var response = await _securityService.ResetPassword(model, CancellationToken.None);

        return response.Success ? Ok() : UnsuccessfulResponse(response);
    }
}
