using SonarMonitor.UseCases.SonarQube;

namespace SonarMonitor.UseCases.Interfaces;

public interface IConsole
{
    void TableReports(IEnumerable<ReportMeasureDto> reports);

    void ReportEnvironments(ReportEnvironmentDto report);

    Task<T> StatusTask<T>(Task<T> task, string processing);

    void Warning(string value);

    void Exception(Exception ex);
}
