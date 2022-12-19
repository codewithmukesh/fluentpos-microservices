using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Auth;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
        services.AddScoped<ICurrentUserService, CurrentUserService>();
}
