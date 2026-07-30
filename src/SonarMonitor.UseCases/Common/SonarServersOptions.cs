namespace SonarMonitor.UseCases.Common;

public class SonarServersOptions
{
    public const string SectionName = "SonarServers";

    public required Dictionary<string, SonarServerConfig> Servers { get; set; }
}

public class SonarServerConfig
{
    public required string Url { get; set; }
    public required string Auth { get; set; }
    public required string Token { get; set; }
}
