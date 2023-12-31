using Tlis.Cms.ShowManagement.Api.Extensions;
using Tlis.Cms.ShowManagement.Application;
using Tlis.Cms.ShowManagement.Infrastructure;

namespace Tlis.Cms.ShowManagement.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();

            builder.Services.ConfigureProblemDetails();
            builder.Services.ConfigureSwagger();
            builder.Services.ConfigureAuthorization(builder.Configuration);

            builder.Logging.ConfigureOtel(builder.Configuration);
            builder.Services.ConfigureOtel(builder.Configuration);

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();
            
            app.UseExceptionHandler();
            app.UseStatusCodePages();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

