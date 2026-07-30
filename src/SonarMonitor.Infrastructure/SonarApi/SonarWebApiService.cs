using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SonarMonitor.UseCases.Common;
using SonarMonitor.UseCases.Interfaces;
using SonarMonitor.UseCases.SonarQube;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SonarMonitor.Infrastructure.SonarApi;

public class SonarWebApiService(
    IHttpClientFactory _httpClientFactory,
    IOptions<SonarQubeOptions> _options,
    ILogger<SonarWebApiService> _logger) : ISonarWebApiService
{
    private const string keyViolations = "violations";
    private const string keyCoverage = "coverage";
    private const string keyLastCommitDate = "last_commit_date";

    private const string ApiMeasuresEndpoint = "api/measures/component";
    private const string ParamMetricKeys = "metricKeys";
    private const string ParamComponent = "component";

    public async Task<SonarMeasuresDto?> GetMeasuresAsync(string versionSonar, string projectKey, CancellationToken cancellationToken)
    {
        var client = CreateSonarHttpClient(versionSonar);
        var sonarUrl = SonarUrlFormat(projectKey);
        var result = await client.GetAsync(sonarUrl, cancellationToken);

        if (!result.IsSuccessStatusCode)
        {
            _logger.LogError("Não foi possível consultar o Sonar");
            return null;
        }

        var text = await result.Content.ReadAsStringAsync(cancellationToken);
        var sonarReponse = JsonSerializer.Deserialize<SonarResponse>(text);

        return CreateSonarMeasures(sonarReponse);
    }

    private HttpClient CreateSonarHttpClient(string versionSonar)
    {
        if (!_options.Value.Servers.TryGetValue(versionSonar, out var sonarConfig))
        {
            throw new InvalidOperationException("Configurar o Servidor do SonarQube.");
        }

        var httpClient = _httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(sonarConfig.Auth, sonarConfig.Token);
        httpClient.BaseAddress = new Uri(sonarConfig.Url);

        return httpClient;
    }

    private static string SonarUrlFormat(string projectKey) =>
        $"{ApiMeasuresEndpoint}?{ParamMetricKeys}={string.Join(",", keyViolations, keyCoverage, keyLastCommitDate)}&{ParamComponent}={projectKey}";

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
