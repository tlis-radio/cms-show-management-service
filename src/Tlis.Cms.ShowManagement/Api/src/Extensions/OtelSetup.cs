using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Tlis.Cms.ShowManagement.Shared;

namespace Tlis.Cms.ShowManagement.Api.Extensions;

public static class OtelSetup
{
    public static void ConfigureOtel(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(Telemetry.ServiceName))
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddEntityFrameworkCoreInstrumentation()
                .AddNpgsql()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(opt =>
                {
                    opt.Endpoint = GetOtelCollectorEndpoint(configuration);
                }));
    }

    public static void ConfigureOtel(this ILoggingBuilder logging, ConfigurationManager configuration)
    {
        logging.AddOpenTelemetry(options =>
        {
            options.SetResourceBuilder(
                ResourceBuilder.CreateDefault().AddService(Telemetry.ServiceName))
                .AddOtlpExporter(opt =>
                {
                    opt.Endpoint = GetOtelCollectorEndpoint(configuration);
                });
        });
    }

    private static Uri GetOtelCollectorEndpoint(ConfigurationManager configuration)
        => new (configuration.GetSection("Otel").GetValue<string>("CollectorEndpoint") ?? throw new NullReferenceException("CollectorEndpoint"));
}