namespace SonarMonitor.Infrastructure.SonarApi;

internal sealed record SonarResponse(
    ComponentData Component
);

internal sealed record ComponentData(
    Measure[] Measures
);

internal sealed record Measure(
    string Metric,
    string Value
);
