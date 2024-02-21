using NetBox.Admin.Core.Security.Dtos;
using NetBox.Admin.Core.Security.Filters;
using NetBox.Admin.SharedKernal.Models;
using NetBox.Admin.SharedKernal.Responses;

namespace NetBox.Admin.Core.Security.Interfaces;

public interface ISecurityService
{
    Task<ResponseResult<AuthenticatedUserDto>> AuthenticateUser(AuthenticateUserDto model, CancellationToken token);
    Task<ResponseResult<UserDto>> CreateUser(UserCreateDto model);
    Task<ResponseResult<UserDto>> GetUser(Guid id, CancellationToken token);
    Task<ResponseResult<IReadOnlyList<UserSummaryDto>>> GetList(Paginator paginator, UserFilter filter, CancellationToken token);
    Task<ResponseResult> UpdateUser(Guid id, UpdateUserDto model, CancellationToken token);
    Task<ResponseResult> ChangeUserPassword(Guid user, UpdateUserPasswordDto model, CancellationToken token);
    Task<ResponseResult<UserProfileDto>> GetUserProfile(Guid userId, CancellationToken token);
    Task<ResponseResult> UpdateUserProfile(Guid userId, UpdateUserProfileDto model, CancellationToken token);
    Task<ResponseResult> DeleteUser(Guid id, CancellationToken token);
    Task<ResponseResult> GetUserNotificationSchedule(Guid userId, CancellationToken token);
    Task<ResponseResult> SendResetPasswordEmail(ForgotPasswordModel forgotPasswordModel, CancellationToken cancellationToken);
    Task<ResponseResult> ResetPassword(ResetPasswordDto model, CancellationToken token);
}
