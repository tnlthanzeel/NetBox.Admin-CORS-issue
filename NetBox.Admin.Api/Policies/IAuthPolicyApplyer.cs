using Microsoft.AspNetCore.Authorization;

namespace NetBox.Admin.Api.Policies;

public interface IAuthPolicyApplyer
{
    void Apply(AuthorizationOptions options);
}
