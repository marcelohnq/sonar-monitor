using Mediator;
using Microsoft.Extensions.Logging;

namespace SonarMonitor.Console.Command;

public class CommandRequest(IMediator _mediator, ILogger<CommandRequest> _logger)
{
    public async Task ExecuteCommand(string[] args)
    {
        switch (args[0])
        {
            case "-l":
                break;
        }
    }
}
