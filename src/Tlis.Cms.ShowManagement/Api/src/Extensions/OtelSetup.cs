using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.ResourceDetectors.Container;
using OpenTelemetry.ResourceDetectors.Host;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Tlis.Cms.ShowManagement.Shared;

namespace Tlis.Cms.ShowManagement.Api.Extensions;

public static class OtelSetup
{
    public static void ConfigureOtel(this IServiceCollection services, IHostEnvironment environment)
    {
        var deploymentEnvironmentAttribute = new KeyValuePair<string, object>("deployment.environment", environment.EnvironmentName);

        services
            .AddOpenTelemetry()
            .WithMetrics(metrics => metrics
                .SetResourceBuilder(
                    ResourceBuilder
                        .CreateEmpty()
                        .AddService(Telemetry.ServiceName)
                        .AddAttributes([ deploymentEnvironmentAttribute ]))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddMeter("Microsoft.AspNetCore.Hosting")
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                .AddOtlpExporter())
            .WithTracing(tracing => tracing
                .SetResourceBuilder(ResourceBuilder
                    .CreateEmpty()
                    .AddService(Telemetry.ServiceName)
                    .AddAttributes([ deploymentEnvironmentAttribute ])
                    .AddDetector(new ContainerResourceDetector())
                    .AddDetector(new HostDetector()))
                .SetSampler(new AlwaysOnSampler())
                .AddAspNetCoreInstrumentation()
                .AddEntityFrameworkCoreInstrumentation()
                .AddNpgsql()
                .AddHttpClientInstrumentation()
                .AddConsoleExporter()
                .AddOtlpExporter());
    }

    public static void ConfigureOtel(this ILoggingBuilder logging, IHostEnvironment environment)
    {
        var deploymentEnvironmentAttribute = new KeyValuePair<string, object>("deployment.environment", environment.EnvironmentName);

        logging.AddOpenTelemetry(options =>
        {
            options.SetResourceBuilder(
                ResourceBuilder
                    .CreateDefault()
                        .AddService(Telemetry.ServiceName)
                        .AddAttributes([ deploymentEnvironmentAttribute ]))
                .AddOtlpExporter();
        });
    }
}