using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tlis.Cms.ShowManagement.Infrastructure.Configurations;
using Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Base;
using Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Dtos;
using Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Interfaces;

namespace Tlis.Cms.ShowManagement.Infrastructure.HttpServices;

internal sealed class UserManagementHttpService(
    HttpClient client,
    IOptions<CmsServicesConfiguration> options)
    : BaseHttpService(client, options.Value.UserManagement), IUserManagementHttpService
{
    public async Task<UserFilterDto> FilterUsersAsync(IEnumerable<Guid> userIds)
    {
        string query = string.Join("&", userIds.Select(x => $"userIds={x}"));

        var response = await GetAsync<UserFilterDto>($"/user/filter?{query}");

        return response ?? new ();
    }
}