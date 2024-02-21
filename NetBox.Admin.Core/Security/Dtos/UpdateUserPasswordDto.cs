namespace NetBox.Admin.Core.Security.Dtos;

public sealed record UpdateUserPasswordDto(string CurrentPassword, string NewPassword, string ConfirmPassword);
