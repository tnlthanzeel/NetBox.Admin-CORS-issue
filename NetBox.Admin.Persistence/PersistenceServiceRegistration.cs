using EntityFramework.Exceptions.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetBox.Admin.Core.Advertisements.Interfaces;
using NetBox.Admin.Core.Common.Interfaces;
using NetBox.Admin.Core.Customers.Interfaces;
using NetBox.Admin.Core.Jobs.Interfaces;
using NetBox.Admin.Core.Security.Interfaces;
using NetBox.Admin.Core.Settings.ClientTypes.Interfaces;
using NetBox.Admin.Core.Settings.DesignSentByModes.Interfaces;
using NetBox.Admin.Core.Settings.JobTypes.Interfaces;
using NetBox.Admin.Core.Settings.Services.Interfaces;
using NetBox.Admin.Persistence.Repositories;
using NetBox.Admin.Persistence.Repositories.Advertisments;
using NetBox.Admin.Persistence.Repositories.Jobs;
using NetBox.Admin.Persistence.Repositories.Security;
using NetBox.Admin.Persistence.Repositories.Settings;
using NetBox.Admin.Persistence.Repositories.Settings.Services;
using NetBox.Admin.Persistence.Repositories.TokenNumber;

namespace NetBox.Admin.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString(AppConstants.Database.APIDbConnectionName))
                  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                  .UseExceptionProcessor());

        services.TryAddScoped<IUserSecurityRespository, UserSecurityRespository>();
        services.TryAddScoped<IUnitOfWork, UnitOfWork>();

        services.TryAddScoped<IServiceTypeRepository, ServiceTypeRepository>();
        services.TryAddScoped<IServicesRepository, ServicesRepository>();
        services.TryAddScoped<IClientTypeRepository, ClientTypeRepository>();
        services.TryAddScoped<IJobTypeRepository, JobTypeRepository>();
        services.TryAddScoped<IDesignSentByModeRepository, DesignSentByModeRepository>();
        services.TryAddScoped<IAdvertismentRepository, AdvertismentRepository>();
        services.TryAddScoped<ICustomerRepository, CustomerRepository>();
        services.TryAddScoped<ITokenNumberGeneratorRepository, TokenNumberGeneratorRepository>();
        services.TryAddScoped<IJobRepository, JobRepository>();
        services.TryAddScoped<IDesginerJobRepository, DesginerJobRepository>();

        return services;
    }
}
