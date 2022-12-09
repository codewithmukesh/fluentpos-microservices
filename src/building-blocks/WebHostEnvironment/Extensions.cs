using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BuildingBlocks.WebHostEnvironment;

public static class Extensions
{
    public static readonly string Development = "Development";
    public static readonly string Production = "Production";
    public static readonly string Docker = "Docker";
    public static readonly string DockerDevelopment = "DockerDevelopment";
    public static bool IsDockerDevelopment(this IWebHostEnvironment webHost)
    {
        if (webHost.IsEnvironment(DockerDevelopment)) return true;
        return false;
    }
}
