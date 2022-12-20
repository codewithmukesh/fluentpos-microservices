using BuildingBlocks.Logging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.CQRS;

public static class Extensions
{
    public static IServiceCollection RegisterMediatR(this IServiceCollection services, Assembly mediatrAssembly, bool enableLoggingBehavior = true)
    {
        services.AddMediatR(mediatrAssembly);
        if (enableLoggingBehavior) services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }
}
