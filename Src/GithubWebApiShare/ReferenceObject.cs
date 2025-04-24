namespace GithubWebApi;

/// <summary>
/// Represents an object associated with a Git reference in a GitHub repository.
/// </summary>
public class ReferenceObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReferenceObject"/> class.
    /// </summary>
    /// <param name="model">The model containing reference object data.</param>
    internal ReferenceObject(ReferenceObjectModel model)
    {
        Sha = model.Sha;
        Type = model.Type;
        Url = model.Url;
    }

    /// <summary>
    /// Gets the SHA hash of the object.
    /// </summary>
    public string? Sha { get; }

    /// <summary>
    /// Gets the type of the object (e.g., "commit" or "tag").
    /// </summary>
    public string? Type { get; }

    /// <summary>
    /// Gets the API URL of the object.
    /// </summary>
    public string? Url { get; }
}
