namespace GithubWebApi;

/// <summary>
/// Represents metadata information for the Github Web API, such as the installed version.
/// </summary>
public class Meta
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Meta"/> class using the specified model.
    /// </summary>
    /// <param name="model">The model containing metadata information.</param>
    internal Meta(MetaModel model)
    {
        this.InstalledVersion = model.InstalledVersion;
    }

    /// <summary>
    /// Gets the installed version of the Github Web API.
    /// </summary>
    public string? InstalledVersion { get; }
}
