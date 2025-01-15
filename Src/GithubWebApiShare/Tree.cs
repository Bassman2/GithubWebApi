namespace GithubWebApi;

[DebuggerDisplay("{Url}")]
public class Tree
{
    internal Tree(TreeModel model)
    {
        this.Sha = model.Sha;
        this.Url = model.Url;
        this.Trees = model.Trees.CastModel<TreeItem>();
        this.Truncated = model.Truncated;
    }

    public string? Sha { get; }

    public string? Url { get; }

    public IEnumerable<TreeItem>? Trees { get; }

    public bool Truncated { get; }
}

