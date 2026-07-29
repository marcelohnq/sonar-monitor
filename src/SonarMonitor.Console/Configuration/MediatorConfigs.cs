using Microsoft.Extensions.DependencyInjection;
using SonarMonitor.UseCases.Quotes.List;

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
                typeof(ListQuotesQuery)
            ];
        });

        return services;
    }
}
