using BuildingBlocks.Auth;
using BuildingBlocks.Configs;
using BuildingBlocks.Web;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.OpenID;
public static class Extensions
{
    public static IServiceCollection RegisterJWTAuth(this IServiceCollection services)
    {
        var authConfig = services.GetRequiredConfiguration<AuthConfig>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = authConfig.Authority;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters.ValidateAudience = false;
            });

        if (!string.IsNullOrEmpty(authConfig.Audience))
        {
            services.AddAuthorization(options =>
                options.AddPolicy(nameof(ApiScope), policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", authConfig.Audience);
                })
            );
        }

        services.AddHttpContextAccessor();
        services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();

        return services;
    }
}
