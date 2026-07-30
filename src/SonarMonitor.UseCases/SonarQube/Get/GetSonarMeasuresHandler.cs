using Mediator;
using SonarMonitor.UseCases.Interfaces;

namespace SonarMonitor.UseCases.SonarQube.Get;

public class GetSonarMeasuresHandler(ISonarWebApiService _sonarWebApiService) : IRequestHandler<GetSonarMeasuresQuery, SonarMeasuresDto?>
{
    public async ValueTask<SonarMeasuresDto?> Handle(GetSonarMeasuresQuery request, CancellationToken cancellationToken)
    {
        return await _sonarWebApiService.GetMeasuresAsync(request.VersionSonar, request.ProjectKey, cancellationToken);
    }
}
