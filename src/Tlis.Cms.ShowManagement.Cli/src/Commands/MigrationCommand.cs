using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tlis.Cms.ShowManagement.Cli.Commands.Base;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence;

namespace Tlis.Cms.ShowManagement.Cli.Commands;

public class MigrationCommand(ShowManagementDbContext dbContext, ILogger<MigrationCommand> logger)
    : BaseCommand("migration", "Run DB migration", logger)
{
    protected override async Task TryHandleCommand()
    {
        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            _logger.LogInformation(
                $"Applying migrations: {string.Join(',', pendingMigrations)}"
            );

            await dbContext.Database.MigrateAsync();
        }
        else
        {
            _logger.LogInformation("No migrations to execute");
        }
    }
}
