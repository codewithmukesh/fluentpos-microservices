using BuildingBlocks.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Serilog.Sinks.SpectreConsole;

namespace BuildingBlocks.Logging;

public static class Extensions
{
    public static WebApplicationBuilder AddCommonLogger(this WebApplicationBuilder builder, IWebHostEnvironment env)
    {
        _ = builder.Host.UseSerilog((context, services, loggerConfiguration) =>
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var loggerOptions = context.Configuration.GetSection(nameof(LoggerOptions)).Get<LoggerOptions>();
            if (loggerOptions is null) throw new ConfigurationNotFoundException(nameof(LoggerOptions));
            var logLevel = Enum.TryParse<LogEventLevel>(
                loggerOptions.Level,
                true,
                out var level) ? level : LogEventLevel.Information;

            loggerConfiguration
                .MinimumLevel.Is(logLevel)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .Enrich.WithExceptionDetails()
                .Enrich.FromLogContext();

            if (loggerOptions.PrintToConsole) loggerConfiguration.WriteTo.SpectreConsole(loggerOptions.LogTemplate, logLevel);
            if (loggerOptions.StructuredLoggerOptions.Enable)
            {
                _ = loggerConfiguration.WriteTo.File(new CompactJsonFormatter(), path: loggerOptions.StructuredLoggerOptions.Path);
            }
        });

        return builder;
    }
}
