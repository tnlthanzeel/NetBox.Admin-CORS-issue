namespace NetBox.Admin.Core.Security.Dtos;

public sealed record UserDto(Guid Id,
    string Email,
    string UserName,
    string FirstName,
    string LastName,
    string TimeZone,
    IReadOnlyList<UserClaimsDto> Claims,
    IReadOnlyList<string> Roles,
    string DisplayName,
    string? NICNumber,
    string? MobileNumber);
