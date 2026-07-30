using Microsoft.Extensions.DependencyInjection;
using SonarMonitor.Infrastructure.Console;
using SonarMonitor.Infrastructure.SonarApi;
using SonarMonitor.UseCases.Common;
using SonarMonitor.UseCases.Interfaces;

namespace SonarMonitor.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
      this IServiceCollection services)
    {
        services.AddOptions<SonarQubeOptions>()
            .BindConfiguration(SonarQubeOptions.SectionName)
            .Validate(o => o.Servers.Count > 0, "Um servidor do SonarQube deve ser informado.")
            .ValidateOnStart();

        services.AddScoped<ISonarWebApiService, SonarWebApiService>();
        services.AddSingleton<IConsole, SpectreConsole>();

        return services;
    }
}