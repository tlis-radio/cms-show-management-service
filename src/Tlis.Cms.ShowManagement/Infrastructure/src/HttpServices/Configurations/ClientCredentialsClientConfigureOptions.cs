using Duende.AccessTokenManagement;
using Microsoft.Extensions.Options;
using Tlis.Cms.ShowManagement.Infrastructure.Configurations;

internal class ClientCredentialsClientConfigureOptions(IOptions<CmsServicesConfiguration> configuration)
    : IConfigureNamedOptions<ClientCredentialsClient>
{
    private readonly CmsServicesConfiguration _configuration = configuration.Value;

    public void Configure(string? name, ClientCredentialsClient options)
    {
        if (name == "show_management")
        {
            options.TokenEndpoint = _configuration.UserManagement.TokenEndpoint;
            options.ClientId = _configuration.UserManagement.ClientId;
            options.ClientSecret = _configuration.UserManagement.ClientSecret;
            options.Parameters.Add("audience", _configuration.UserManagement.BaseAddress);
        }
    }

    public void Configure(ClientCredentialsClient options)
    {
        throw new System.NotImplementedException();
    }
}