namespace NetBox.Admin.Api.Policies;

public sealed class AdvertismentPolicies : IAuthPolicyApplyer
{
    public void Apply(AuthorizationOptions options)
    {
        options.AddPolicy(ApplicationAuthPolicy.Advertisment.Create,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Advertisment.Create));
                        });

        options.AddPolicy(ApplicationAuthPolicy.Advertisment.Delete,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Advertisment.Delete));
                        });


        options.AddPolicy(ApplicationAuthPolicy.Advertisment.View,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Advertisment.View));
                        });
    }
}
