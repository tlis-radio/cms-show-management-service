using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tlis.Cms.ShowManagement.Infrastructure.HttpServices;
using Tlis.Cms.ShowManagement.Infrastructure.HttpServices.Interfaces;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.ShowManagement.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Duende.AccessTokenManagement;

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

        services.AddClientCredentialsTokenManagement();
        services.AddSingleton<IConfigureOptions<ClientCredentialsClient>, ClientCredentialsClientConfigureOptions>();
            // .AddClient("show_management", client =>
            // {
            //     client.TokenEndpoint = "https://tlis.eu.auth0.com/oauth/token";
            //     client.ClientId = "JU79zQavUuwjayP8TelP9fDpVztr89Em";
            //     client.ClientSecret = "zmSkpMIDhNdthmCFrdmtGFAlmW415F_ptF5NTJm5VErPF4qmmN1uQKdoHk96e0SB";
            //     client.Parameters.Add("audience", "https://localhost:7152");
            // });

        services
            .AddHttpClient<IUserManagementHttpService, UserManagementHttpService>()
            .AddClientCredentialsTokenHandler("show_management")
            .AddStandardResilienceHandler();
    }
}