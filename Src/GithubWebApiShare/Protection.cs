namespace GithubWebApi;

/// <summary>
/// Represents the protection settings of a branch in a GitHub repository.
/// </summary>
public class Protection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Protection"/> class.
    /// </summary>
    internal Protection()
    { }

    /// <summary>
    /// Gets or sets a value indicating whether branch protection is enabled.
    /// </summary>
    public bool? Enabled { get; set; }

    /// <summary>
    /// Gets or sets the required status checks for the branch.
    /// </summary>
    public RequiredStatusChecks? RequiredStatusChecks { get; set; }
}
