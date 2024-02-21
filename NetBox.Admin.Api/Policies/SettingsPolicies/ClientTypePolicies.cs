namespace NetBox.Admin.Api.Policies.SettingsPolicies;

public sealed class ClientTypePolicies : IAuthPolicyApplyer
{
    public void Apply(AuthorizationOptions options)
    {
        options.AddPolicy(ApplicationAuthPolicy.Settings.ClientType.Create,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.ClientType.Create));
                        });

        options.AddPolicy(ApplicationAuthPolicy.Settings.ClientType.Delete,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.ClientType.Delete));
                        });


        options.AddPolicy(ApplicationAuthPolicy.Settings.ClientType.View,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.ClientType.View));
                        });


        options.AddPolicy(ApplicationAuthPolicy.Settings.ClientType.Update,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.ClientType.Update));
                        });
    }
}
