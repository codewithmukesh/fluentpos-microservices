using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BuildingBlocks.WebHostEnvironment;

public static class Extensions
{
    public static readonly string Development = "development";
    public static readonly string Production = "production";
    public static readonly string Docker = "docker";
    public static readonly string DockerDevelopment = "docker.development";
    public static bool IsDockerDevelopment(this IWebHostEnvironment webHost)
    {
        return webHost.IsEnvironment(DockerDevelopment);
    }
}
