﻿using Microsoft.AspNetCore.Identity;
using NetBox.Admin.Core.Security.Dtos;
using NetBox.Admin.Core.Security.Entities;
using System.Security.Claims;

namespace NetBox.Admin.Core.Security.Interfaces;

public interface IUserSecurityRespository : IBaseRepository
{
    Task<IReadOnlyList<UserClaimsDto>> GetUserClaims(Guid userId, CancellationToken token);

    Task<IReadOnlyList<string>> GetUserRoles(ApplicationUser user, CancellationToken token = default);

    Task<IReadOnlyList<Role>> GetAllRoles(CancellationToken token = default, bool asTracking = false, string? searchQuery = null);

    Task<bool> HasClaim(Guid userId, IEnumerable<string> claimValue, CancellationToken token = default);

    Task<ApplicationUser?> GetUser(ClaimsPrincipal user);

    Task<ApplicationUser?> FindByEmail(string email);

    Task<ResponseResult<UserDto>> CreateUser(string userName, string password, string email, string role, string firstName, string lastName, IEnumerable<string> permissions, string timeZone, string displayName, string? nicNumber, string? mobileNumber);

    Task<ApplicationUser?> GetUser(Guid id, CancellationToken token, bool asTracking = false);

    Task<bool> DoesRoleExists(string roleName, CancellationToken token);

    Task<(IReadOnlyList<ApplicationUser> list, int totalRecocrds)> GetUserListBySpec(Paginator paginator, ISpecification<ApplicationUser> specification, CancellationToken token, bool asTracking = false);

    Task<IReadOnlyList<UserAssignedRole>> GetUsersWithRoles(IEnumerable<Guid> userIds);

    Task<ResponseResult> UpdateIdentityUserUser(ApplicationUser user, string email, string role, IEnumerable<string> permissions, CancellationToken token);

    Task<ApplicationUser?> GetUserBySpec(Guid id, CancellationToken token, ISpecification<ApplicationUser> specification, bool asTracking = false);

    Task<Role?> GetRoleById(Guid id, CancellationToken token, bool asTracking = false);
    Task<ResponseResult<UserRoleDto>> CreateRole(string roleName, CancellationToken token);

    Task<ResponseResult> DeleteRole(Guid roleId, CancellationToken token);
    Task DeleteRoleClaimsForRole(Role userRole);

    IReadOnlyList<IdentityRoleClaim<Guid>> AddRoleClaimsForRole(Guid id, IEnumerable<string> permissions);

    Task<IReadOnlyList<UserClaim>> DeleteUserClaimsForRoleClaim(Role userRole);

    void AddUserClaimsForRoleClaims(IEnumerable<IdentityRoleClaim<Guid>> newRoleClaims, IEnumerable<Guid> userIds);

    Task<List<IdentityRoleClaim<Guid>>> GetRoleClaimsByRoleId(Guid roleId);

    Task MergeClaims(List<Guid> userIds, List<string> distinctPermissions);

    Task<IReadOnlyList<IdentityRoleClaim<Guid>>> GetRoleClaims(CancellationToken token);

    Task<ResponseResult> ChangeUserPassword(ApplicationUser user, string currentPassword, string newPassword, CancellationToken token = default);

    Task<bool> IsInRoleAsync(ApplicationUser user, string roleName);

    Task<TResult?> GetUserByIdWithProjectionSpec<TResult>(ISpecification<ApplicationUser, TResult> specification, CancellationToken token);

    Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
    Task<ResponseResult> ResetPassword(ApplicationUser user, string decodedPasswrdResetToken, string newPassword);

    Task<IReadOnlyList<IdentityUserRole<Guid>>> GetUsersInRole(Role role, CancellationToken cancellationToken = default);
    Task<ResponseResult<UserRoleDto>> UpdateRole(Guid id, string roleName, CancellationToken token);
    Task<ApplicationUser?> FindByEmailOrUsername(string email, CancellationToken cancellationToken = new());
}
