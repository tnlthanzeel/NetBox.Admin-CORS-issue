using System.ComponentModel.DataAnnotations;

namespace NetBox.Admin.Core.Security;

public sealed class JwtConfig
{

    [Required]
    public string Issuer { get; init; } = null!;
    [Required]
    public string Audience { get; init; } = null!;
    [Required]
    public string SigningKey { get; init; } = null!;
    [Required]
    public TimeSpan TokenLifetime { get; init; }
}