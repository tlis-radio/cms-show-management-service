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
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddControllers();

            builder.Services.ConfigureProblemDetails();
            builder.Services.ConfigureSwagger();
            builder.Services.ConfigureAuthorization(builder.Configuration);

            builder.Logging.AddConsole();
            builder.Logging.ConfigureOtel(builder.Environment);
            builder.Services.ConfigureOtel(builder.Environment);

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

