using NetBox.Admin.Core.Security;

namespace NetBox.Admin.Api.Services;

internal sealed class ConfigurationValidator
{
    public static void ValidateConfigurations(IServiceCollection services)
    {
        services.AddOptions<JwtConfig>()
                .BindConfiguration(nameof(JwtConfig))
                .ValidateDataAnnotations()
                .ValidateOnStart();
    }
}
