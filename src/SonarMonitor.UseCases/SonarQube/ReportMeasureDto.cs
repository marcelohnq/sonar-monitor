namespace SonarMonitor.UseCases.SonarQube;

public record ReportEnvironmentDto(
    IEnumerable<ReportMeasureDto> Developments,
    IEnumerable<ReportMeasureDto> Releases);

public record ReportMeasureDto(
    string Name,
    SonarMeasuresDto? Measures);