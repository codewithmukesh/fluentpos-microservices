using Microsoft.AspNetCore.Builder;

namespace BuildingBlocks.Swagger;

public static class Extensions
{
    public static IApplicationBuilder UseCommonSwagger(this IApplicationBuilder app)
    {
        return app;
    }
}