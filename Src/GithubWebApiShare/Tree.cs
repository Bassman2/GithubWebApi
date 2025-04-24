namespace GithubWebApi;


/// <summary>
/// Represents a Git tree in a GitHub repository.
/// </summary>
[DebuggerDisplay("{Url}")]
public class Tree
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tree"/> class.
    /// </summary>
    /// <param name="model">The model containing tree data.</param>
    internal Tree(TreeModel model)
    {
        this.Sha = model.Sha;
        this.Url = model.Url;
        this.Trees = model.Trees.CastModel<TreeItem>();
        this.Truncated = model.Truncated;
    }

    /// <summary>
    /// Gets the SHA hash of the tree.
    /// </summary>
    public string? Sha { get; }

    /// <summary>
    /// Gets the API URL of the tree.
    /// </summary>
    public string? Url { get; }

    /// <summary>
    /// Gets the list of tree items (files, directories, etc.) in the tree.
    /// </summary>
    public List<TreeItem>? Trees { get; }

    /// <summary>
    /// Gets a value indicating whether the tree is truncated.
    /// </summary>
    public bool Truncated { get; }
}

