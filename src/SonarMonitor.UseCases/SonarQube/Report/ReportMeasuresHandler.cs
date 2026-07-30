using Mediator;
using Microsoft.Extensions.Options;
using SonarMonitor.UseCases.Common;
using SonarMonitor.UseCases.Interfaces;

namespace SonarMonitor.UseCases.SonarQube.Report;

public class ReportMeasuresHandler(
    ISonarWebApiService _sonarWebApiService,
    IOptions<SonarQubeOptions> _options,
    IConsole _console) : IRequestHandler<ReportMeasuresQuery, IEnumerable<ReportMeasureDto>>
{
    public async ValueTask<IEnumerable<ReportMeasureDto>> Handle(ReportMeasuresQuery request, CancellationToken cancellationToken)
    {
        var measuresDevelopments = await GetMeasures(_options.Value.Developments, cancellationToken);
        var measuresReleases = await GetMeasures(_options.Value.Releases, cancellationToken);

        return [.. measuresDevelopments, .. measuresReleases];
    }

    private async Task<IEnumerable<ReportMeasureDto>> GetMeasures(Dictionary<string, SonarProjectConfig> projects, CancellationToken cancellationToken)
    {
        var reports = new List<ReportMeasureDto>();

        foreach (var project in projects)
        {
            var measure = await _console.StatusTask(
                _sonarWebApiService.GetMeasuresAsync(project.Value.Sonar, project.Value.Key, cancellationToken),
                project.Key);

            reports.Add(new(project.Key, measure));
        }

        return reports;
    }
}