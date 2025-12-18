namespace GithubWebApi;

/// <summary>
/// Represents a GitHub repository.
/// </summary>
[DebuggerDisplay("{FullName}")]
public class Repository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Repository"/> class.
    /// </summary>
    /// <param name="model">The model containing repository data.</param>
    internal Repository(RepositoryModel model)
    {
        this.Id = model.Id;
        this.NodeId = model.NodeId;
        this.Name = model.Name;
        this.FullName = model.FullName;
        this.Owner = model.Owner.CastModel<User>();
        this.Private = model.Private;
        this.HtmlUrl = model.HtmlUrl;
        this.Description = model.Description;
        this.Fork = model.Fork;
        this.Url = model.Url;
        this.ArchiveUrl = model.ArchiveUrl;
    }

    /// <summary>
    /// Gets the unique identifier of the repository.
    /// </summary>
    public long Id { get; internal init; }

    /// <summary>
    /// Gets the Node ID of the repository.
    /// </summary>
    public string? NodeId { get; internal init; }

    /// <summary>
    /// Gets the name of the repository.
    /// </summary>
    public string? Name { get; internal init; }

    /// <summary>
    /// Gets the full name of the repository, including the owner (e.g., "owner/repo").
    /// </summary>
    public string? FullName { get; internal init; }

    /// <summary>
    /// Gets the owner of the repository.
    /// </summary>
    public User? Owner { get; internal init; }

    /// <summary>
    /// Gets a value indicating whether the repository is private.
    /// </summary>
    public bool? Private { get; internal init; }

    /// <summary>
    /// Gets the HTML URL of the repository.
    /// </summary>
    public string? HtmlUrl { get; internal init; }

    /// <summary>
    /// Gets the description of the repository.
    /// </summary>
    public string? Description { get; internal init; }

    /// <summary>
    /// Gets a value indicating whether the repository is a fork.
    /// </summary>
    public bool? Fork { get; internal init; }

    /// <summary>
    /// Gets the API URL of the repository.
    /// </summary>
    public string? Url { get; internal init; }

    /// <summary>
    /// Gets the archive URL template of the repository.
    /// </summary>
    public string? ArchiveUrl { get; internal init; }
}
