using BuildingBlocks.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BuildingBlocks.EFCore;

public static class Extensions
{
    public static IServiceCollection AddEFCoreDbContext<T>(this IServiceCollection services, IConfiguration configuration, Database databaseChoice)
        where T : DbContext
    {
        var assemblyName = typeof(T).Assembly.GetName().Name;
        var connectionString = configuration.GetConnectionString(Constants.DefaultConnection);
        switch (databaseChoice)
        {
            case Database.PostgreSQL:
                services.AddDbContext<T>(o => o.UseNpgsql(connectionString, m => m.MigrationsAssembly(assemblyName)));
                break;
            case Database.SQLServer:
                break;
            default:
                break;
        }
        return services;
    }

    public static IApplicationBuilder UseEFCoreMigration<T>(this IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
    where T : DbContext
    {
        MigrateDatabaseAsync<T>(applicationBuilder.ApplicationServices).GetAwaiter().GetResult();
        if (webHostEnvironment.IsDevelopment())
        {
            SeedDataAsync(applicationBuilder.ApplicationServices).GetAwaiter().GetResult();
        }
        return applicationBuilder;
    }
    private static async Task MigrateDatabaseAsync<T>(IServiceProvider serviceProvider)
        where T : DbContext
    {
        ILogger logger = Log.ForContext(typeof(Extensions));
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<T>();
        if (context.Database.GetPendingMigrations().Any())
        {
            logger.Information($"Applying Migrations for {typeof(T).Name}.");
            await context.Database.MigrateAsync();
        }
    }
    private static async Task SeedDataAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();
        foreach (var seeder in seeders)
        {
            await seeder.SeedAllAsync();
        }
    }
}
