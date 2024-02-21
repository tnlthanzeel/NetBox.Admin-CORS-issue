using NetBox.Admin.Api.PolicyRequriements.UserClaimRequirements;
using NetBox.Admin.Core.Security.Claims;

namespace NetBox.Admin.Api.Policies.SettingsPolicies;

public sealed class ServicePolicies : IAuthPolicyApplyer
{
    public void Apply(AuthorizationOptions options)
    {
        options.AddPolicy(ApplicationAuthPolicy.Settings.Services.Create,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.Service.Create));
                        });

        options.AddPolicy(ApplicationAuthPolicy.Settings.Services.Delete,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.Service.Delete));
                        });


        options.AddPolicy(ApplicationAuthPolicy.Settings.Services.View,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.Service.View));
                        });


        options.AddPolicy(ApplicationAuthPolicy.Settings.Services.Update,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.Service.Update));
                        });
    }
}
