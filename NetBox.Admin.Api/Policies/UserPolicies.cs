using NetBox.Admin.Api.PolicyRequriements.UserClaimRequirements;
using NetBox.Admin.Core.Security.AuthPolicies;
using NetBox.Admin.Core.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace NetBox.Admin.Api.Policies;

public sealed class UserPolicies : IAuthPolicyApplyer
{
    public void Apply(AuthorizationOptions options)
    {
        options.AddPolicy(ApplicationAuthPolicy.UserPolicy.Create,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.User.Create));
                        });

        options.AddPolicy(ApplicationAuthPolicy.UserPolicy.View,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.User.View));
                        });

        options.AddPolicy(ApplicationAuthPolicy.UserPolicy.Edit,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.User.Edit));
                        });

        options.AddPolicy(ApplicationAuthPolicy.UserPolicy.Delete,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.User.Delete));
                        });
    }
}
