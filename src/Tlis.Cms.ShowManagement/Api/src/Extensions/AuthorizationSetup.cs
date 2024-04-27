using Microsoft.AspNetCore.Authentication.JwtBearer;
using Tlis.Cms.ShowManagement.Api.Constants;

namespace Tlis.Cms.ShowManagement.Api.Extensions;

public static class AuthorizationSetup
{
    public static void ConfigureAuthorization(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = configuration.GetSection("Jwt").GetValue<string>("Authority");
                options.Audience = configuration.GetSection("Jwt").GetValue<string>("Audience");
                options.RequireHttpsMetadata = configuration.GetSection("Jwt").GetValue<bool>("RequireHttpsMetadata");
                options.SaveToken = true;
            });
        
        services.AddAuthorizationBuilder()
            .AddPolicy(Policy.ShowWrite, policy => policy.RequireClaim("permissions", "write:show"))
            .AddPolicy(Policy.ShowDelete, policy => policy.RequireClaim("permissions", "delete:show"))
            .AddPolicy(Policy.ShowRead, policy => policy.RequireClaim("permissions", "read:show"));
    }
}