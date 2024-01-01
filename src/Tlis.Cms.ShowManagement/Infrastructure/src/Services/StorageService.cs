using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tlis.Cms.ShowManagement.Infrastructure.Configurations;
using Tlis.Cms.ShowManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.ShowManagement.Infrastructure.Services;

internal sealed class StorageService : IStorageService
{
    private const string profileImagesContainer = "show-profile-images";
    
    private readonly string _storageAccountUrl;

    private readonly BlobContainerClient _profileImagesContainerClient;
    
    private readonly ILogger<StorageService> _logger;

    public StorageService(
        IConfiguration configuration,
        ILogger<StorageService> logger,
        IOptions<ServiceUrlsConfiguration> serviceUrlsConfiguration)
    {
        _logger = logger;
        _storageAccountUrl = serviceUrlsConfiguration.Value.StorageAccount;
        _profileImagesContainerClient = new BlobContainerClient(
            configuration.GetConnectionString("StorageAccount"),
            profileImagesContainer);
    }
    
    public Task<bool> DeleteProfileImage(string fileUrl)
        => DeleteFile(_profileImagesContainerClient, fileUrl);


    public Task<(Guid, string)> UploadProfileImage(IFormFile file)
        => UploadFile(_profileImagesContainerClient, file, profileImagesContainer);
    
    private async Task<bool> DeleteFile(BlobContainerClient client, string fileUrl)
    {
        try
        {
            var response = await client.DeleteBlobAsync(fileUrl.Split('/').Last());

            return response.Status == 202;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            return false;
        }
    }

    private Task<(Guid, string)> UploadFile(BlobContainerClient client, IFormFile file, string containerName, bool overrideFileName = true)
        => UploadFile(client, file.OpenReadStream(), file.FileName, file.ContentType, containerName, overrideFileName);

    private async Task<(Guid, string)> UploadFile(
        BlobContainerClient client,
        Stream stream,
        string fileName,
        string contentType,
        string containerName,
        bool overrideFileName = true)
    {
        var guid = Guid.NewGuid();
        var storageFileName = overrideFileName ? GetStorageFileName(guid, fileName) : fileName;
        var blob = client.GetBlobClient(storageFileName);

        await blob.UploadAsync(stream, new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = contentType
            }
        });

        return (guid, Path.Combine(_storageAccountUrl, containerName, storageFileName));
    }

    private string GetStorageFileName(Guid guid, string fileName) => $"{guid}{Path.GetExtension(fileName)}";
}