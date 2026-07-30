namespace SonarMonitor.UseCases.Common;

public class SonarProjectsOptions
{
    public const string SectionName = "SonarProjects";

    public required Dictionary<string, SonarProjectsEnvironment> Environments { get; set; }
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
