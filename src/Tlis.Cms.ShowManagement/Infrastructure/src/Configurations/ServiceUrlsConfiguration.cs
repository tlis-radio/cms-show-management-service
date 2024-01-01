using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.ShowManagement.Infrastructure.Configurations;

internal sealed class ServiceUrlsConfiguration
{
    [Required(AllowEmptyStrings = false)]
    public string StorageAccount { get; set; } = null!;
}