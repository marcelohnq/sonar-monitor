namespace SonarMonitor.UseCases.SonarQube;

public record ReportMeasureDto(
    string Name,
    SonarMeasuresDto? Measures);

