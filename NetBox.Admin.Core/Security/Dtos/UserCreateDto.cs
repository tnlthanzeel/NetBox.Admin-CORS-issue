namespace NetBox.Admin.Core.Security.Dtos;

public sealed record UserCreateDto(
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword,
    string FirstName,
    string LastName,
    string Role,
    string TimeZone,
    IEnumerable<string> Permissions,
    string DisplayName,
    string? NICNumber,
    string? MobileNumber);

