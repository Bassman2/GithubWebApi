namespace GithubWebApi;

/// <summary>
/// Represents a Git reference in a GitHub repository.
/// </summary>
public class Reference
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Reference"/> class.
    /// </summary>
    /// <param name="model">The model containing reference data.</param>
    internal Reference(ReferenceModel model)
    {
        Ref = model.Ref;
        NodeId = model.NodeId;
        Url = model.Url;
        Object = model.Object.CastModel<ReferenceObject>();
    }

    /// <summary>
    /// Gets the fully qualified reference name (e.g., "refs/heads/main").
    /// </summary>
    public string? Ref { get; }

    /// <summary>
    /// Gets the Node ID of the reference.
    /// </summary>
    public string? NodeId { get; }

    /// <summary>
    /// Gets the API URL of the reference.
    /// </summary>
    public string? Url { get; }

    /// <summary>
    /// Gets the object associated with the reference (e.g., a commit or tag).
    /// </summary>
    public ReferenceObject? Object { get; }
}
