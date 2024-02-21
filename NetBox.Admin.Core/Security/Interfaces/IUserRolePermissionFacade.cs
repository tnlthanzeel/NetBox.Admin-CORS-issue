using NetBox.Admin.Core.Security.Dtos;

namespace NetBox.Admin.Core.Security.Interfaces;

public interface IUserRolePermissionFacadeService
{
    Task<ResponseResult> UpdateRole(Guid roleId, UpdateRoleDto model, CancellationToken token);
}
