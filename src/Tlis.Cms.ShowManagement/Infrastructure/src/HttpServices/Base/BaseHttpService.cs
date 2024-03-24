using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Tlis.Cms.ShowManagement.Infrastructure.Configurations;

namespace Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Base;

internal abstract class BaseHttpService
{
    private readonly HttpServiceConfiguration _configuration;

    private readonly HttpClient _client;

    public BaseHttpService(HttpClient client, HttpServiceConfiguration configuration)
    {
        _configuration = configuration;
        _client = client;

        _client.BaseAddress = new Uri(_configuration.BaseAddress);
    }

    public Task<T?> GetAsync<T>(string requestUri) => _client.GetFromJsonAsync<T>(requestUri);
}