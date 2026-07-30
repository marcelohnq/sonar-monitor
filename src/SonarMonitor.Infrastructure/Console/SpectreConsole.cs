using SonarMonitor.UseCases.Interfaces;
using SonarMonitor.UseCases.SonarQube;
using Spectre.Console;

namespace SonarMonitor.Infrastructure.Console;

public class SpectreConsole : IConsole
{
    public void Warning(string value)
    {
        AnsiConsole.MarkupLineInterpolated($"[#FFA500]⚠[/] [yellow]{value}[/]");
    }

    public void Exception(Exception ex)
    {
        AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything | ExceptionFormats.ShowLinks);
    }

    public async Task<T> StatusTask<T>(Task<T> task, string processing)
    {
        return await AnsiConsole.Status()
            .StartAsync($"Processando {processing}...", async ctx =>
            {
                T result = await task;

                AnsiConsole.MarkupLineInterpolated($"[green]Feito![/]");

                return result;
            });
    }

    public void TableReports(IEnumerable<ReportMeasureDto> reports)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);

        table.AddColumn("Projeto");
        table.AddColumn("Violações");
        table.AddColumn("Cobertura");
        table.AddColumn("Última");

        foreach (var report in reports)
        {
            var violations = report.Measures?.Violations.ToString() ?? string.Empty;
            var coverage = report.Measures?.Coverage.HasValue == true ? report.Measures.Coverage.Value.ToString("0.00'%'") : string.Empty;
            var lastCommit = report.Measures?.LastCommit.HasValue == true ? report.Measures.LastCommit.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty;

            table.AddRow(report.Name, violations, coverage, lastCommit);
        }
        
        AnsiConsole.Write(table);
    }
}
