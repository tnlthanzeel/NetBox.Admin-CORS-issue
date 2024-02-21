namespace NetBox.Admin.Core.Security.Dtos;

public record UpdateRoleDto(string RoleName,IEnumerable<string> Permissions);
