namespace GithubWebApi;

/// <summary>
/// Represents a Git tag in a GitHub repository.
/// </summary>
public class Tag
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tag"/> class.
    /// </summary>
    /// <param name="model">The model containing tag data.</param>
    internal Tag(TagModel model)
    {
        this.Name = model.Name;
        this.ZipballUrl = model.ZipballUrl;
        this.TarballUrl = model.TarballUrl;
        this.Commit = model.Commit.CastModel<Commit>();
        this.NodeId = model.NodeId;
    }

    /// <summary>
    /// Gets the name of the tag.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Gets the URL to download the tag as a ZIP archive.
    /// </summary>
    public string? ZipballUrl { get; }

    /// <summary>
    /// Gets the URL to download the tag as a TAR archive.
    /// </summary>
    public string? TarballUrl { get; }

    /// <summary>
    /// Gets the commit associated with the tag.
    /// </summary>
    public Commit? Commit { get; }

    /// <summary>
    /// Gets the Node ID of the tag.
    /// </summary>
    public string? NodeId { get; }
}
