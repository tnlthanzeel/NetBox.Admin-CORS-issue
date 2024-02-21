namespace NetBox.Admin.Core.Security.Dtos;

public sealed class UserClaimsDto
{
    public string ClaimType { get; init; } = null!;

    public string ClaimValue { get; init; } = null!;
}
