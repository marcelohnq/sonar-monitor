using System.Text.Json.Serialization;

namespace SonarMonitor.Infrastructure.SonarApi;

internal sealed record SonarResponse(
    [property: JsonPropertyName("component")] ComponentData Component
);

internal sealed record ComponentData(
    [property: JsonPropertyName("measures")] Measure[] Measures
);

internal sealed record Measure(
    [property: JsonPropertyName("metric")] string Metric,
    [property: JsonPropertyName("value")] string Value
);