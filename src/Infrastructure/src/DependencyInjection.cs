using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tlis.Cms.ShowManagement.Infrastructure.Configurations;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.ShowManagement.Infrastructure.Services;
using Tlis.Cms.ShowManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.ShowManagement.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<ServiceUrlsConfiguration>()
            .Bind(configuration.GetSection("ServiceUrls"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext<IShowManagementDbContext, ShowManagementDbContext>(options =>
            {
                options
                    .UseNpgsql(
                        configuration.GetConnectionString("Postgres"),
                        x => x.MigrationsHistoryTable(
                            HistoryRepository.DefaultTableName, 
                            "cms_show_management"))
                    .UseSnakeCaseNamingConvention();
            },
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Singleton);
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IStorageService, StorageService>();
    }
}