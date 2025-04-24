namespace GithubWebApi;

/// <summary>
/// Represents the required status checks for a protected branch in a GitHub repository.
/// </summary>
public class RequiredStatusChecks
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RequiredStatusChecks"/> class.
    /// </summary>
    /// <param name="model">The model containing required status checks data.</param>
    internal RequiredStatusChecks(RequiredStatusChecksModel model)
    {
        this.EnforcementLevel = model.EnforcementLevel;
        this.Contexts = model.Contexts;
        this.Checks = model.Checks;
    }

    /// <summary>
    /// Gets the enforcement level of the required status checks (e.g., "non_admins").
    /// </summary>
    public string? EnforcementLevel { get; }

    /// <summary>
    /// Gets the list of status check contexts that must pass before merging.
    /// </summary>
    public List<string>? Contexts { get; }

    /// <summary>
    /// Gets the list of individual checks that are required to pass.
    /// </summary>
    public List<string>? Checks { get; }
}
