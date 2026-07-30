using Mediator;
using SonarMonitor.UseCases.Interfaces;
using SonarMonitor.UseCases.SonarQube.Get;
using SonarMonitor.UseCases.SonarQube.Report;

namespace SonarMonitor.Console.Command;

public class CommandRequest(IMediator _mediator, IConsole _console)
{
    public async Task ExecuteCommand(string[] args)
    {
        if (args.Length < 1)
        {
            _console.Warning("Nenhum argumento foi informada - Encerrando programa.");
            return;
        }

        try
        {
            switch (args[0])
            {
                case "-s": // Single
                    if (args.Length > 1)
                    {
                        var measures = await _mediator.Send(new GetSonarMeasuresQuery(args[1], args[2]));
                        _console.TableReports([new (args[2], measures)]);
                    }
                    break;

                case "-r": // Report
                    var reportEnvironments = await _mediator.Send(new ReportMeasuresQuery());
                    _console.ReportEnvironments(reportEnvironments);
                    break;

                case "-m": // Mail
                    break;
            }
        }
        catch (Exception ex)
        {
            _console.Exception(ex);
        }
    }
}
