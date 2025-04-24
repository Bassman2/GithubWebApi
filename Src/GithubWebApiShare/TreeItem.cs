namespace GithubWebApi;

/// <summary>
/// Represents an item in a Git tree, such as a file or directory, in a GitHub repository.
/// </summary>
[DebuggerDisplay("{Path}")]
public class TreeItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TreeItem"/> class.
    /// </summary>
    /// <param name="model">The model containing tree item data.</param>
    internal TreeItem(TreeItemModel model)
    {
        this.Path = model.Path;
        this.Mode = model.Mode;
        this.Type = model.Type;
        this.Size = model.Size;
        this.Sha = model.Sha;
        this.Url = model.Url;
    }

    /// <summary>
    /// Gets the path of the item in the repository.
    /// </summary>
    public string? Path { get; }

    /// <summary>
    /// Gets the file mode of the item (e.g., "100644" for a file, "040000" for a directory).
    /// </summary>
    public string? Mode { get; }

    /// <summary>
    /// Gets the type of the item (e.g., "blob" for a file, "tree" for a directory).
    /// </summary>
    public string? Type { get; }

    /// <summary>
    /// Gets the size of the item in bytes. This is only applicable for files.
    /// </summary>
    public long? Size { get; }

    /// <summary>
    /// Gets the SHA hash of the item.
    /// </summary>
    public string? Sha { get; }

    /// <summary>
    /// Gets the API URL of the item.
    /// </summary>
    public string? Url { get; }
}
