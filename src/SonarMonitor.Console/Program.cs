using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SonarMonitor.Console.Command;
using SonarMonitor.Console.Configuration;
using SonarMonitor.Infrastructure;

var builder = CreateApplicationBuilder(args);
using IHost host = builder.Build();

using var scope = host.Services.CreateScope();
var command = scope.ServiceProvider.GetRequiredService<CommandRequest>();
await command.ExecuteCommand(args);

await host.StopAsync();

public static partial class Program
{
    public static HostApplicationBuilder CreateApplicationBuilder(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);

        builder.Services.AddHttpClient();
        builder.Services.AddServicesConfigs(builder);
        builder.Services.AddMediatorSourceGen();
        builder.Services.AddInfrastructureServices();

        return builder;
    }
}