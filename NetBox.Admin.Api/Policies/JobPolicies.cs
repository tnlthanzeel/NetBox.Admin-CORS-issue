namespace NetBox.Admin.Api.Policies;

public sealed class JobPolicies : IAuthPolicyApplyer
{
    public void Apply(AuthorizationOptions options)
    {
        options.AddPolicy(ApplicationAuthPolicy.Job.Create,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Job.Create));
                        });

        options.AddPolicy(ApplicationAuthPolicy.Job.View,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Job.View));
                        });

        options.AddPolicy(ApplicationAuthPolicy.Job.Edit,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Job.Edit));
                        });

        options.AddPolicy(ApplicationAuthPolicy.Job.Delete,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Job.Delete));
                        });
    }
}
