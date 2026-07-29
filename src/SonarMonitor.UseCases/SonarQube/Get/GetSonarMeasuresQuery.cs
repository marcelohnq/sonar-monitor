using Mediator;

namespace SonarMonitor.UseCases.SonarQube.Get;

public record GetSonarMeasuresQuery(string VersionSonar, string ProjectKey) : IRequest<SonarMeasuresDto?>;
