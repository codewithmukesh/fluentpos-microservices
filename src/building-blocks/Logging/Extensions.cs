using BuildingBlocks.Configs;
using BuildingBlocks.Constants;
using BuildingBlocks.Exceptions;
using Figgle;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SpectreConsole;

namespace BuildingBlocks.Logging;

public static class Extensions
{
    public static WebApplicationBuilder RegisterSerilog(this WebApplicationBuilder builder, IWebHostEnvironment env, string? appName)
    {
        _ = builder.Host.UseSerilog((context, services, loggerConfiguration) =>
        {
            var loggerOptions = context.Configuration.GetSection(nameof(LoggerConfig)).Get<LoggerConfig>();
            if (loggerOptions is null) throw new ConfigurationNotFoundException(nameof(LoggerConfig));
            var logLevel = Enum.TryParse<LogEventLevel>(
                loggerOptions.Level,
                true,
                out var level) ? level : LogEventLevel.Information;

            loggerConfiguration
                .MinimumLevel.Is(logLevel)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("MassTransit", LogEventLevel.Warning)
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .Enrich.FromLogContext();

            if (loggerOptions.PrintToConsole) loggerConfiguration.WriteTo.SpectreConsole(loggerOptions.LogTemplate, logLevel);
            if (loggerOptions.StructuredLoggerOptions.Enable)
            {
                _ = loggerConfiguration.WriteTo.File(new CompactJsonFormatter(), path: loggerOptions.StructuredLoggerOptions.Path);
            }

            if (loggerOptions.ElasticSearchConfig.Enable)
            {
                var formattedAppName = appName?.ToLower().Replace(".", "-").Replace(" ", "-");
                var indexFormat = $"{formattedAppName}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}";
                _ = loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(loggerOptions.ElasticSearchConfig.Uri))
                {
                    IndexFormat = indexFormat,
                    AutoRegisterTemplate = true,
                    NumberOfShards = 2,
                    NumberOfReplicas = 1,
                    MinimumLogEventLevel = LogEventLevel.Information,
                }).Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName!);
            }
        });

        if (appName is null)
        {
            Console.WriteLine(FiggleFonts.Standard.Render(ApplicationDefaults.Name));
        }
        else
        {
            Console.WriteLine(FiggleFonts.Standard.Render(appName));
        }

        return builder;
    }
}
