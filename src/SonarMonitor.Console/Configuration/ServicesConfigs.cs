using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SonarMonitor.Console.Command;
using SonarMonitor.Infrastructure.Email;
using SonarMonitor.UseCases.Interfaces;

namespace SonarMonitor.Console.Configuration;

public static class ServicesConfigs
{
    public static IServiceCollection AddServicesConfigs(this IServiceCollection services, IHostApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            services.AddScoped<IEmailSender, FakeEmailSender>();
        }
        else
        {
            services.AddScoped<IEmailSender, EmailSender>();
        }

        services.AddScoped<CommandRequest>();

        return services;
    }
}
