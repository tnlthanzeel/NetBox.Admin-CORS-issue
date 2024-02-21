
using NetBox.Admin.Infrastructure.FileServices;
using NetBox.Admin.Infrastructure.NotificationServices;
using NetBox.Admin.SharedKernal.Interfaces;
namespace NetBox.Admin.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            opt.NotificationPublisher = new TaskWhenAllPublisher();
            opt.NotificationPublisherType = typeof(TaskWhenAllPublisher);
        });

        services.TryAddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();

        return services;
    }
}
