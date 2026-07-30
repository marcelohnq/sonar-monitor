using Mediator;

namespace SonarMonitor.UseCases.SonarQube.Report;

public record ReportMeasuresQuery() : IRequest<ReportEnvironmentDto>;
