namespace NetBox.Admin.Api.Policies.SettingsPolicies;

public sealed class DesignSentByMode : IAuthPolicyApplyer
{
    public void Apply(AuthorizationOptions options)
    {
        options.AddPolicy(ApplicationAuthPolicy.Settings.DesignSentByMode.Create,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.DesignSentByMode.Create));
                        });

        options.AddPolicy(ApplicationAuthPolicy.Settings.DesignSentByMode.Delete,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.DesignSentByMode.Delete));
                        });


        options.AddPolicy(ApplicationAuthPolicy.Settings.DesignSentByMode.View,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.DesignSentByMode.View));
                        });


        options.AddPolicy(ApplicationAuthPolicy.Settings.DesignSentByMode.Update,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.DesignSentByMode.Update));
                        });
    }
}
