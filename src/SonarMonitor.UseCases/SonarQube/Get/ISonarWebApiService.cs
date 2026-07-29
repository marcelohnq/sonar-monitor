namespace SonarMonitor.UseCases.SonarQube.Get;

public interface ISonarWebApiService
{
    Task<SonarMeasuresDto?> GetMeasuresAsync(string versionSonar, string projectKey, CancellationToken cancellationToken);
}