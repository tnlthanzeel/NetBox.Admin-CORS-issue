using NetBox.Admin.Core.Advertisements.Interfaces;
using NetBox.Admin.Core.Advertisements.Services;
using NetBox.Admin.Core.Customers;
using NetBox.Admin.Core.Customers.Interfaces;
using NetBox.Admin.Core.Jobs.Interfaces;
using NetBox.Admin.Core.Jobs.Services;
using NetBox.Admin.Core.Lookups;
using NetBox.Admin.Core.Security.Services;
using NetBox.Admin.Core.Settings.ClientTypes.Interfaces;
using NetBox.Admin.Core.Settings.ClientTypes.Services;
using NetBox.Admin.Core.Settings.DesignSentByModes.Interfaces;
using NetBox.Admin.Core.Settings.DesignSentByModes.Services;
using NetBox.Admin.Core.Settings.JobTypes.Interfaces;
using NetBox.Admin.Core.Settings.JobTypes.Services;
using NetBox.Admin.Core.Settings.Services.Interfaces;
using NetBox.Admin.Core.Settings.Services.Services;

namespace NetBox.Admin.Core;

public static class CoreServiceRegistration
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            opt.NotificationPublisher = new TaskWhenAllPublisher();
            opt.NotificationPublisherType = typeof(TaskWhenAllPublisher);
        });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.TryAddScoped<IModelValidator, ModelValidator>();

        services.TryAddScoped<ISecurityService, SecurityService>();
        services.TryAddScoped<ITokenBuilder, TokenBuilder>();
        services.TryAddScoped<IPermissionService, PermissionService>();
        services.TryAddScoped<IUserRoleService, UserRoleService>();
        services.TryAddScoped<IUserRolePermissionFacadeService, UserRolePermissionFacadeService>();

        services.TryAddScoped<IServiceTypeService, ServiceTypeService>();
        services.TryAddScoped<IServicesService, ServicesService>();
        services.TryAddScoped<IClientTypeService, ClientTypeService>();
        services.TryAddScoped<IJobTypeService, JobTypeService>();
        services.TryAddScoped<IDesignSentByModeService, DesignSentByModeService>();
        services.TryAddScoped<IMainDisplayService, MainDisplayService>();
        services.TryAddScoped<ILookupService, LookupService>();
        services.TryAddScoped<ICusotmerService, CusotmerService>();
        services.TryAddScoped<ITokenNumberGenerator, TokenNumberGenerator>();
        services.TryAddScoped<IJobManager, JobManager>();
        services.TryAddScoped<IDesignerJobService, DesignerJobService>();
        services.TryAddScoped<IDesginerAssginedJobManager, DesginerAssginedJobManager>();

        return services;
    }
}
