﻿using NetBox.Admin.Core.Security.Dtos;
using NetBox.Admin.SharedKernal.Responses;

namespace NetBox.Admin.Core.Security.Interfaces;

public interface IUserRoleService
{
    Task<ResponseResult<UserRoleDto>> CreateRole(UserRoleCreateDto model, CancellationToken token);
    Task<ResponseResult> Delete(Guid id, CancellationToken token);
    Task<ResponseResult<IReadOnlyList<UserRoleDto>>> GetAllRoles(string? searchQuery, CancellationToken token);
    Task<ResponseResult<UserRoleDto>> GetById(Guid id, CancellationToken token);
    Task<ResponseResult<IReadOnlyList<PermissionTemplateDto>>> GetRoleClaimTemplates(CancellationToken token);
    Task<ResponseResult<PermissionTemplateDto>> GetRoleClaimTemplate(Guid roleId, CancellationToken token);
}
