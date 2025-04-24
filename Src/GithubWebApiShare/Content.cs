namespace GithubWebApi;

/// <summary>
/// Represents the content of a file or directory in a GitHub repository.
/// </summary>
public class Content
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Content"/> class.
    /// </summary>
    /// <param name="model">The model containing content data.</param>
    internal Content(ContentModel model)
    {
        this.Type = model.Type;
        this.Encoding = model.Encoding;
        this.Size = model.Size;
        this.Name = model.Name;
        this.Path = model.Path;
        this.Content_ = model.Content;
        this.Sha = model.Sha;
        this.Url = model.Url;
        this.GitUrl = model.GitUrl;
        this.HtmlUrl = model.HtmlUrl;
        this.DownloadUrl = model.DownloadUrl;
        this.Links = model.Links.CastModel<Links>();
    }

    /// <summary>
    /// Gets the type of the content (e.g., "file" or "dir").
    /// </summary>
    public string? Type { get; }

    /// <summary>
    /// Gets the encoding of the content (e.g., "base64").
    /// </summary>
    public string? Encoding { get; }

    /// <summary>
    /// Gets the size of the content in bytes.
    /// </summary>
    public long? Size { get; }

    /// <summary>
    /// Gets the name of the content.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Gets the path of the content in the repository.
    /// </summary>
    public string? Path { get; }

    /// <summary>
    /// Gets the actual content as a string.
    /// </summary>
    public string? Content_ { get; }

    /// <summary>
    /// Gets the SHA hash of the content.
    /// </summary>
    public string? Sha { get; }

    /// <summary>
    /// Gets the API URL of the content.
    /// </summary>
    public string? Url { get; }

    /// <summary>
    /// Gets the Git URL of the content.
    /// </summary>
    public string? GitUrl { get; }

    /// <summary>
    /// Gets the HTML URL of the content.
    /// </summary>
    public string? HtmlUrl { get; }

    /// <summary>
    /// Gets the download URL of the content.
    /// </summary>
    public string? DownloadUrl { get; }

    /// <summary>
    /// Gets the links associated with the content.
    /// </summary>
    public Links? Links { get; }
}
