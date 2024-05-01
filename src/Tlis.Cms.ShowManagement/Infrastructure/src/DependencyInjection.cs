using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tlis.Cms.ShowManagement.Infrastructure.HttpServices;
using Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Interfaces;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.ShowManagement.Infrastructure.Configurations;

namespace Tlis.Cms.ShowManagement.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<CmsServicesConfiguration>()
            .Bind(configuration.GetSection("CmsServices"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext(configuration);
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services
            .AddHttpClient<IUserManagementHttpService, UserManagementHttpService>()
            .AddStandardResilienceHandler();
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IShowManagementDbContext, ShowManagementDbContext>(options =>
            {
                options
                    .UseNpgsql(
                        configuration.GetConnectionString("Postgres"),
                        x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, ShowManagementDbContext.SCHEMA))
                    .UseSnakeCaseNamingConvention();
            },
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Singleton);
    }
}