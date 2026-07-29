using Mediator;
using Microsoft.Extensions.Logging;
using SonarMonitor.UseCases.SonarQube;
using SonarMonitor.UseCases.SonarQube.Get;

namespace SonarMonitor.Console.Command;

public class CommandRequest(IMediator _mediator, ILogger<CommandRequest> _logger)
{
    private const string PrintFormat = "{LastCommit} - [{Key}] V: {Violations} | C: {Coverage}";

    public async Task ExecuteCommand(string[] args)
    {
        switch (args[0])
        {
            case "-u":
                if (args.Length > 1)
                {
                    var measures = await _mediator.Send(new GetSonarMeasuresQuery(args[1], args[2]));
                    PrintMeasures(args[2], measures);
                }
                break;
        }
    }

    private void PrintMeasures(string key, SonarMeasuresDto? sonarMeasures)
    {
        _logger.LogInformation(PrintFormat, sonarMeasures?.LastCommit, key, sonarMeasures?.Violations, sonarMeasures?.Coverage);
    }
}
