using BuildingBlocks.Constants;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Events;
public static class Extensions
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        var eventBusSettings = configuration.GetSection(nameof(EventBusSettings)).Get<EventBusSettings>();
        if (eventBusSettings is null) throw new ConfigurationNotFoundException(nameof(EventBusSettings));
        services.AddMassTransit(config =>
        {
            config.AddConsumers(Assembly.GetEntryAssembly());
            if (eventBusSettings.RMQ?.Enable == true)
            {
                config.UsingRabbitMq((context, configurator) =>
                            {
                                configurator.Host(new Uri($"rabbitmq://{eventBusSettings.RMQ.Host}"), h =>
                                {
                                    h.Username(eventBusSettings.RMQ.Username);
                                    h.Password(eventBusSettings.RMQ.Password);
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
