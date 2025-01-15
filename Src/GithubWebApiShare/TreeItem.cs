namespace GithubWebApi;

[DebuggerDisplay("{Path}")]
public class TreeItem
{
    internal TreeItem(TreeItemModel model)
    {
        this.Path = model.Path;
        this.Mode = model.Mode;
        this.Type = model.Type;
        this.Size = model.Size;
        this.Sha = model.Sha;
        this.Url = model.Url;
    }

    public string? Path { get; }

    public string? Mode { get; }

    public string? Type { get; }

    public long? Size { get; }

    public string? Sha { get; }

    public string? Url { get; }
}
