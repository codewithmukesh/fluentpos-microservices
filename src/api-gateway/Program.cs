using BuildingBlocks.Logging;
using BuildingBlocks.OpenID;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);
builder.AddCommonLoging(builder.Environment);
builder.Services.AddJWT();
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();
app.MapGet("/", () => "Hello From Gateway");
app.UseStaticFiles(); // The middleware runs before routing happens => no route was found

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();
app.MapReverseProxy(config =>
{
    config.Use(async (context, next) =>
    {
        var token = await context.GetTokenAsync("access_token");
        context.Request.Headers["Authorization"] = $"Bearer {token}";

        await next().ConfigureAwait(false);
    });
});

app.Run();
