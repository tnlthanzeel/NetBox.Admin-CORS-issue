namespace NetBox.Admin.Core.Security.Dtos;

public sealed record UpdateUserDto
    (
    string Email,
    string FirstName,
    string LastName,
    string Role,
    string TimeZone,
    IEnumerable<string> Permissions,
    string? NICNumber,
    string? MobileNumber,
    string DisplayName);
