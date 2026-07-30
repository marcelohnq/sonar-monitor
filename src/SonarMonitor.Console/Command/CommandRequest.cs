using Mediator;
using Microsoft.Extensions.Logging;
using SonarMonitor.UseCases.SonarQube;
using SonarMonitor.UseCases.SonarQube.Get;
using SonarMonitor.UseCases.SonarQube.Report;

namespace SonarMonitor.Console.Command;

public class CommandRequest(
    IMediator _mediator,
    ILogger<CommandRequest> _logger)
{
    private const string PrintFormat = "Violacoes: {Violations} | Cobertura: {Coverage} | Ultima: {LastCommit}";

    public async Task ExecuteCommand(string[] args)
    {
        switch (args[0])
        {
            case "-s": // Single
                if (args.Length > 1)
                {
                    var measures = await _mediator.Send(new GetSonarMeasuresQuery(args[1], args[2]));
                    PrintMeasures(args[2], measures);
                }
                break;
            case "-r": // Report
                var reports = await _mediator.Send(new ReportMeasuresQuery());

                foreach (var report in reports)
                {
                    PrintMeasures(report.Name, report.Measures);
                }
                break;
            case "-m": // Mail
                break;
        }
    }

    private void PrintMeasures(string key, SonarMeasuresDto? sonarMeasures)
    {
        var violations = sonarMeasures?.Violations.ToString() ?? string.Empty;
        var coverage = sonarMeasures?.Coverage.HasValue == true ? sonarMeasures.Coverage.Value.ToString("P1") : string.Empty;
        var lastCommit = sonarMeasures?.LastCommit.HasValue == true ? sonarMeasures?.LastCommit.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty;

        _logger.LogInformation("Projeto {Projeto}", key);
        _logger.LogInformation(PrintFormat, violations, coverage, lastCommit);
    }
}
