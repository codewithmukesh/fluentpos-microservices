using BuildingBlocks.Configs;
using BuildingBlocks.Constants;
using BuildingBlocks.Exceptions;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Events;
public static class Extensions
{
    public static IServiceCollection RegisterEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        var eventBusConfig = configuration.GetSection(nameof(EventBusConfig)).Get<EventBusConfig>();
        if (eventBusConfig is null) throw new ConfigurationNotFoundException(nameof(EventBusConfig));
        services.AddMassTransit(config =>
        {
            config.AddConsumers(Assembly.GetEntryAssembly());
            if (eventBusConfig.RMQ?.Enable == true)
            {
                config.UsingRabbitMq((context, configurator) =>
                            {
                                configurator.Host(new Uri($"rabbitmq://{eventBusConfig.RMQ.Host}"), h =>
                                {
                                    h.Username(eventBusConfig.RMQ.Username);
                                    h.Password(eventBusConfig.RMQ.Password);
                                });
                                configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(ApplicationDefaults.Name, false));
                            });
            }
        });
        services.AddOptions<MassTransitHostOptions>()
        .Configure(options =>
        {
            options.WaitUntilStarted = true;
            options.StartTimeout = TimeSpan.FromSeconds(5);
            options.StopTimeout = TimeSpan.FromSeconds(30);
        });
        services.AddTransient<IEventBus, MassTransitEventBus>();

        return services;
    }
}
