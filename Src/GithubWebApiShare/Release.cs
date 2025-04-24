namespace GithubWebApi;

/// <summary>
/// Represents a release in a GitHub repository.
/// </summary>
public class Release
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Release"/> class.
    /// </summary>
    /// <param name="model">The model containing release data.</param>
    internal Release(ReleaseModel model)
    {
        this.Ref = model.Ref;
    }

    /// <summary>
    /// Gets the Git reference associated with the release (e.g., "refs/tags/v1.0.0").
    /// </summary>
    public string? Ref { get; }
}
