using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Tlis.Cms.ShowManagement.Shared;

namespace Tlis.Cms.ShowManagement.Api.Extensions;

public static class OtelSetup
{
    public static void ConfigureOtel(this IServiceCollection services)
    {
        services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(Telemetry.ServiceName))
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter())
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddEntityFrameworkCoreInstrumentation()
                .AddNpgsql()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter());
    }

    public static void ConfigureOtel(this ILoggingBuilder logging)
    {
        logging.AddOpenTelemetry(options =>
        {
            options.SetResourceBuilder(
                ResourceBuilder.CreateDefault().AddService(Telemetry.ServiceName))
                .AddOtlpExporter();
        });
    }
}