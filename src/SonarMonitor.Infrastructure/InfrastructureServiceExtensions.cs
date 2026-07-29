using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SonarMonitor.Infrastructure.SonarApi;
using SonarMonitor.UseCases.SonarQube.Get;

namespace SonarMonitor.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
      this IServiceCollection services,
      ILogger logger)
    {
        services.AddScoped<ISonarWebApiService, SonarWebApiService>();

        logger.LogInformation("Infrastructure - serviços registrados com sucesso");

        return services;
    }
}