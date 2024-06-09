using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tlis.Cms.ShowManagement.Infrastructure.Configurations;
using Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Base;
using Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Dtos;
using Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Interfaces;

namespace Tlis.Cms.ShowManagement.Infrastructure.HttpServices;

internal sealed class ImageManagementHttpService(
    HttpClient client,
    IOptions<CmsServicesConfiguration> options)
    : BaseHttpService(client, options.Value.ImageAssetManagement), IImageManagementHttpService
{
    public Task<ImageDto> GetImageAsync(Guid id) => GetAsync<ImageDto>($"/image/{id}");
}