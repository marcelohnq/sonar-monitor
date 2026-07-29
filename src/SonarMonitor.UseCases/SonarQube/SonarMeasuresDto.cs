namespace SonarMonitor.UseCases.SonarQube;

public record SonarMeasuresDto(
    int? Violations,
    double? Coverage,
    DateTimeOffset? LastCommit);
