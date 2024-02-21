namespace NetBox.Admin.Core.Security.Dtos;

public sealed class AuthenticatedUserDto
{
    public Guid UserId { get; init; }
    public string UserName { get; init; } = null!;
    public string BearerToken { get; init; } = null!;
    public bool IsAuthenticated { get; init; }
    public IReadOnlyList<UserClaimsDto> Claims { get; init; } = new List<UserClaimsDto>();
    public IReadOnlyList<string> Roles { get; init; } = new List<string>();
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
}
