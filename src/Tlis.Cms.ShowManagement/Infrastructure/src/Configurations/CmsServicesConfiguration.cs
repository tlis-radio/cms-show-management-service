namespace Tlis.Cms.ShowManagement.Infrastructure.Configurations;

internal sealed class CmsServicesConfiguration
{
    public required HttpServiceConfiguration UserManagement { get; set; }

    public required HttpServiceConfiguration ImageAssetManagement { get; set; }
}