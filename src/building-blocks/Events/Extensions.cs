using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Events;
public static class Extensions
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumers(Assembly.GetEntryAssembly());
            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(configuration["MessagingOptions:RabbitMQ"]);
                configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("fluentpos", false));
            });
        });
        services.AddOptions<MassTransitHostOptions>()
        .Configure(options =>
        {
            options.WaitUntilStarted = true;
            options.StartTimeout = TimeSpan.FromSeconds(10);
            options.StopTimeout = TimeSpan.FromSeconds(30);
        });
        services.AddTransient<IEventBus, MassTransitEventBus>();

        return services;
    }
}
