using NetBox.Admin.Core.Security.Entities;

namespace NetBox.Admin.Core.Security.Interfaces;

public interface ITokenBuilder
{
    Task<string> GenerateJwtTokenAsync(ApplicationUser user, CancellationToken token);
}
