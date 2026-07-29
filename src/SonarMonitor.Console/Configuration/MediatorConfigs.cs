using Microsoft.Extensions.DependencyInjection;
using SonarMonitor.UseCases.SonarQube.Get;

namespace SonarMonitor.Console.Configuration;

public static class MediatorConfigs
{
    public static IServiceCollection AddMediatorSourceGen(this IServiceCollection services)
    {
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;

            options.Assemblies =
            [
                typeof(GetSonarMeasuresQuery)
            ];
        });

        return services;
    }
}
