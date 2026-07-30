using Mediator;
using SonarMonitor.UseCases.Interfaces;

namespace SonarMonitor.UseCases.SonarQube.Get;

public class GetSonarMeasuresHandler(ISonarWebApiService _sonarWebApiService, IConsole _console) : IRequestHandler<GetSonarMeasuresQuery, SonarMeasuresDto?>
{
    public async ValueTask<SonarMeasuresDto?> Handle(GetSonarMeasuresQuery request, CancellationToken cancellationToken)
    {
        return await _console.StatusTask(
            _sonarWebApiService.GetMeasuresAsync(request.VersionSonar, request.ProjectKey, cancellationToken),
            request.ProjectKey);
    }
}
