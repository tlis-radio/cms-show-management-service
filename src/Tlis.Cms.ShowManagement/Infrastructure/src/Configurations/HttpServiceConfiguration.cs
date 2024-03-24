namespace Tlis.Cms.ShowManagement.Infrastructure.Configurations;

internal sealed class HttpServiceConfiguration
{
    public required string BaseAddress { get; set; }

    public required string TokenEndpoint { get; set; }

    public required string ClientId { get; set; }

    public required string ClientSecret { get; set; }
}