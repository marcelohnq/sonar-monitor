namespace SonarMonitor.UseCases.Common;

public class SonarQubeOptions
{
    public const string SectionName = "SonarQube";

    public Dictionary<string, SonarServerConfig> Servers { get; set; } = [];
    public Dictionary<string, SonarProjectConfig> Developments { get; set; } = [];
    public Dictionary<string, SonarProjectConfig> Releases { get; set; } = [];
}

public class SonarServerConfig
{
    public string Url { get; set; } = string.Empty;
    public string Auth { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}

public class SonarProjectConfig
{
    public required string Sonar { get; set; }
    public required string Key { get; set; }
}