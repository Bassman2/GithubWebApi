namespace GithubWebApi;

public class RequiredStatusChecks
{
    internal RequiredStatusChecks(RequiredStatusChecksModel model)
    {
        this.EnforcementLevel = model.EnforcementLevel;
        this.Contexts = model.Contexts;
        this.Checks = model.Checks;
    }

    public string? EnforcementLevel { get; }

    public List<string>? Contexts { get; }

    public List<string>? Checks { get; }
}
