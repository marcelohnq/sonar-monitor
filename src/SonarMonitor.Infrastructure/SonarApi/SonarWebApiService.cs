using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SonarMonitor.UseCases.SonarQube;
using SonarMonitor.UseCases.SonarQube.Get;
using System.Text.Json;

namespace SonarMonitor.Infrastructure.SonarApi;

public class SonarWebApiService : ISonarWebApiService
{
    private const string keyViolations = "violations";
    private const string keyCoverage = "coverage";
    private const string keyLastCommitDate = "last_commit_date";

    private const string ApiMeasuresEndpoint = "api/measures/component";
    private const string ParamMetricKeys = "metricKeys";
    private const string ParamComponent = "component";

    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    private readonly ILogger<SonarWebApiService> _logger;

    public SonarWebApiService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        ILogger<SonarWebApiService> logger)
    {
        _client = httpClientFactory.CreateClient();
        _configuration = configuration;
        _logger = logger;

        _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
    }

    public async Task<SonarMeasuresDto?> GetMeasuresAsync(string versionSonar, string projectKey, CancellationToken cancellationToken)
    {
        var sonarUrl = SonarUrlFormat(versionSonar, projectKey);

        var result = await _client.GetAsync(sonarUrl, cancellationToken);

        if (!result.IsSuccessStatusCode)
        {
            _logger.LogError("Não foi possível consultar o Sonar");
            return null;
        }

        var text = await result.Content.ReadAsStringAsync();
        var sonarReponse = JsonSerializer.Deserialize<SonarResponse>(text);

        return CreateSonarMeasures(sonarReponse);
    }

    private string SonarUrlFormat(string versionSonar, string projectKey)
    {
        var sonarUrl = _configuration[$"SonarQube:Urls:{versionSonar}"];

        return $"{sonarUrl}/{ApiMeasuresEndpoint}?{ParamMetricKeys}={string.Join(",", keyViolations, keyCoverage, keyLastCommitDate)}&{ParamComponent}={projectKey}";
    }

    private static SonarMeasuresDto? CreateSonarMeasures(SonarResponse? response)
    {
        var violationsString = response?.Component.Measures.FirstOrDefault(m => m.Metric == keyViolations)?.Value;
        var coverageString = response?.Component.Measures.FirstOrDefault(m => m.Metric == keyCoverage)?.Value;
        var lastCommitString = response?.Component.Measures.FirstOrDefault(m => m.Metric == keyLastCommitDate)?.Value;

        _ = int.TryParse(violationsString, out var violations);
        _ = double.TryParse(coverageString, out var coverage);

        DateTimeOffset? dateTimeOffset = long.TryParse(lastCommitString, out var lastCommit)
            ? DateTimeOffset.FromUnixTimeMilliseconds(lastCommit)
            : null;

        return new(
            violations,
            coverage,
            dateTimeOffset);
    }
}
