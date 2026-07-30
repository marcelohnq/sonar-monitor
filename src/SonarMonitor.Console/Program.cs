using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SonarMonitor.Console.Command;
using SonarMonitor.Console.Configuration;
using SonarMonitor.Infrastructure;

var builder = CreateApplicationBuilder(args, out ILogger logger);
using IHost host = builder.Build();

if (args.Length < 1)
{
    logger.LogInformation("Nenhum argumento foi informada - Encerrando programa.");
    return;
}

using var scope = host.Services.CreateScope();
var command = scope.ServiceProvider.GetRequiredService<CommandRequest>();
await command.ExecuteCommand(args);

await host.StopAsync();

public static partial class Program
{
    public static HostApplicationBuilder CreateApplicationBuilder(string[] args, out ILogger logger)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);

        builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

        logger = builder.Logging.Services.BuildServiceProvider()
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger(nameof(Program));

        logger.LogInformation("Start - Sonar Monitor sendo iniciado!");

        builder.Services.AddHttpClient();
        builder.Services.AddServicesConfigs(builder);
        builder.Services.AddMediatorSourceGen();
        builder.Services.AddInfrastructureServices(logger);

        return builder;
    }
}