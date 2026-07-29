using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SonarMonitor.Infrastructure.SonarApi;

public class SonarWebApiService
{
    private readonly HttpClient _client;
    private readonly ILogger<SonarWebApiService> _logger;

    public SonarWebApiService(IHttpClientFactory httpClientFactory, ILogger<SonarWebApiService> logger)
    {
        _client = httpClientFactory.CreateClient();
        _logger = logger;

        _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
    }

    public async Task<decimal?> GetCurrentQuote(string ticker)
    {
        var result = await _client.GetAsync(ticker);

        if (!result.IsSuccessStatusCode)
        {
            _logger.LogError("Não foi possível consultar o Sonar");
            return null;
        }

        var text = await result.Content.ReadAsStringAsync();
        var jObject = JsonSerializer.Deserialize<JsonObject>(text);

        var regularPrice = jObject?["chart"]?["result"]?[0]?["meta"]?["regularMarketPrice"];

        return regularPrice is not null && decimal.TryParse(regularPrice.ToString(), CultureInfo.InvariantCulture, out decimal resultDecimal) ?
            resultDecimal :
            null;
    }
}
