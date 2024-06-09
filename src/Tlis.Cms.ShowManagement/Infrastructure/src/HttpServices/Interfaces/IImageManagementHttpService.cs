using System;
using System.Threading.Tasks;
using Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Dtos;

namespace Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Interfaces;

public interface IImageManagementHttpService
{
    Task<ImageDto> GetImageAsync(Guid id);
}