using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tlis.Cms.ShowManagement.Cli.Commands.Base;
using Tlis.Cms.ShowManagement.Infrastructure.Persistence;

namespace Tlis.Cms.ShowManagement.Cli.Commands;

public class MigrationCommand : BaseCommand
{
    private readonly ShowManagementDbContext _dbContext;

    public MigrationCommand(ShowManagementDbContext dbContext, ILogger<MigrationCommand> logger)
        : base("migration", "Run DB migration", logger)
    {
        _dbContext = dbContext;
    }

    protected override async Task TryHandleCommand()
    {
        var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            _logger.LogInformation(
                $"Applying migrations: {string.Join(',', pendingMigrations)}"
            );

            await _dbContext.Database.MigrateAsync();
        }
        else
        {
            _logger.LogInformation("No migrations to execute");
        }
    }
}
