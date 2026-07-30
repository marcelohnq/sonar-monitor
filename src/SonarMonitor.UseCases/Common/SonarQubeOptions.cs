namespace SonarMonitor.UseCases.Common;

public class SonarQubeOptions
{
    public const string SectionName = "SonarQube";

    public Dictionary<string, SonarServerConfig> Servers { get; set; } = [];
    public Dictionary<string, SonarProjectsEnvironment> Developments { get; set; } = [];
    public Dictionary<string, SonarProjectsEnvironment> Releases { get; set; } = [];
}

public class SonarServerConfig
{
    public string Url { get; set; } = string.Empty;
    public string Auth { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}

public class SonarProjectsEnvironment
{
    public required Dictionary<string, SonarProjectConfig> Projects { get; set; }
}

public class SonarProjectConfig
{
    public required string Sonar { get; set; }
    public required string Key { get; set; }
}