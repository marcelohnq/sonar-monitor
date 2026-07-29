using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SonarMonitor.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
      this IServiceCollection services,
      ConfigurationManager config,
      ILogger logger)
    {
        //services.AddScoped<IQueryListQuote, QueryListQuote>();

        logger.LogInformation("Infrastructure - serviços registrados com sucesso");

        return services;
    }
}