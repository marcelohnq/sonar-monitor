using Mediator;

namespace SonarMonitor.UseCases.SonarQube.Get;

public class GetSonarMeasuresHandler(ISonarWebApiService sonarWebApiService) : IRequestHandler<GetSonarMeasuresQuery, SonarMeasuresDto?>
{
    public async ValueTask<SonarMeasuresDto?> Handle(GetSonarMeasuresQuery request, CancellationToken cancellationToken)
    {
        return await sonarWebApiService.GetMeasuresAsync(request.VersionSonar, request.ProjectKey, cancellationToken);
    }
}
