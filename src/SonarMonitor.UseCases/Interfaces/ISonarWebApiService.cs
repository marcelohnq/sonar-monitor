using SonarMonitor.UseCases.SonarQube;

namespace SonarMonitor.UseCases.Interfaces;

public interface ISonarWebApiService
{
    Task<SonarMeasuresDto?> GetMeasuresAsync(string versionSonar, string projectKey, CancellationToken cancellationToken);
}