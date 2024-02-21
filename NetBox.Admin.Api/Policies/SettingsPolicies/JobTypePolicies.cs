namespace NetBox.Admin.Api.Policies.SettingsPolicies;

public sealed class JobTypePolicies : IAuthPolicyApplyer
{
    public void Apply(AuthorizationOptions options)
    {
        options.AddPolicy(ApplicationAuthPolicy.Settings.JobType.Create,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.JobType.Create));
                        });

        options.AddPolicy(ApplicationAuthPolicy.Settings.JobType.Delete,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.JobType.Delete));
                        });


        options.AddPolicy(ApplicationAuthPolicy.Settings.JobType.View,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.JobType.View));
                        });


        options.AddPolicy(ApplicationAuthPolicy.Settings.JobType.Update,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Settings.JobType.Update));
                        });
    }
}
