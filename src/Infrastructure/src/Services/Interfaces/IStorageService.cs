using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Tlis.Cms.ShowManagement.Infrastructure.Services.Interfaces;

public interface IStorageService
{
    public Task<bool> DeleteProfileImage(string fileUrl);

    public Task<(Guid, string)> UploadProfileImage(IFormFile file);
}