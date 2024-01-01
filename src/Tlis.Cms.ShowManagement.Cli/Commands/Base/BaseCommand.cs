using System.CommandLine;
using Microsoft.Extensions.Logging;

namespace Tlis.Cms.ShowManagement.Cli.Commands.Base;

public abstract class BaseCommand : Command
{
    protected readonly ILogger<BaseCommand> _logger;

    public BaseCommand(string name, string description, ILogger<BaseCommand> logger)
        : base(name, description)
    {
        _logger = logger;
        this.SetHandler(async () => await HandleCommand());
    }

    protected async Task<int> HandleCommand()
    {
        try
        {
            await TryHandleCommand();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to execute command.");
            return 0;
        }

        return 1;
    }

    protected abstract Task TryHandleCommand();
}
