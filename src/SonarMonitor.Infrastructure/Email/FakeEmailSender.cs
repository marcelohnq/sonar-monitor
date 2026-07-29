using SonarMonitor.UseCases.Interfaces;

namespace SonarMonitor.Infrastructure.Email;

public class FakeEmailSender : IEmailSender
{
    public Task SendEmailAsync(string subject, string body)
    {
        return Task.CompletedTask;
    }
}
