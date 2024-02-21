﻿using Microsoft.AspNetCore.Identity;
using NetBox.Admin.Core.Security.Dtos;
using NetBox.Admin.Core.Security.Entities;
using NetBox.Admin.Core.Security.Interfaces;
using NetBox.Admin.Core.Security.Validators;

namespace NetBox.Admin.Core.Security.Services;

public sealed class UserRolePermissionFacadeService : IUserRolePermissionFacadeService
{
    private readonly IModelValidator _validator;
    private readonly RoleManager<Role> _roleManager;
    private readonly IUserSecurityRespository _userSecurityRespository;

    public UserRolePermissionFacadeService(IModelValidator validator, RoleManager<Role> roleManager, IUserSecurityRespository userSecurityRespository)
    {
        _validator = validator;
        _roleManager = roleManager;
        _userSecurityRespository = userSecurityRespository;
    }

    public async Task<ResponseResult> UpdateRole(Guid roleId, UpdateRoleDto model, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync<UpdateRoleClaimsDtoValidator, UpdateRoleDto>(model, token);

        if (validationResult.IsValid is false) return new ResponseResult(validationResult.Errors);

        var role = await _userSecurityRespository.GetRoleById(roleId, token);

        if (role is null) return new ResponseResult(new BadRequestException(nameof(roleId), $"Invalid role id ({roleId})"));

        var roleUpdateResponse = await _userSecurityRespository.UpdateRole(role.Id, model.RoleName, token);

        if (roleUpdateResponse.Success is false) return new(roleUpdateResponse.Errors);

        await _userSecurityRespository.DeleteUserClaimsForRoleClaim(role);

        await _userSecurityRespository.DeleteRoleClaimsForRole(role);

        var distinctPermissions = model.Permissions.Distinct().ToList();

        var newRoleClaims = _userSecurityRespository.AddRoleClaimsForRole(role.Id, distinctPermissions);

        await _userSecurityRespository.SaveChangesAsync(token);

        var usersInRole = await _userSecurityRespository.GetUsersInRole(role, token);

        var userIds = usersInRole.Select(s => s.UserId).Distinct().ToList();

        _userSecurityRespository.AddUserClaimsForRoleClaims(newRoleClaims, userIds);

        await _userSecurityRespository.MergeClaims(userIds, distinctPermissions);

        await _userSecurityRespository.SaveChangesAsync(token);

        return new ResponseResult();
    }
}
