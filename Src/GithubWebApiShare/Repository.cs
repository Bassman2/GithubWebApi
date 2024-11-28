namespace GithubWebApi;

public class Repository
{
    internal Repository(RepositoryModel model)
    {
        this.Id = model.Id;
        this.NodeId = model.NodeId;
        this.Name = model.Name;
        this.FullName = model.FullName;
        this.Owner = model.Owner is not null ? new User(model.Owner) : null;
        this.Private = model.Private;
        this.HtmlUrl = model.HtmlUrl;
        this.Description = model.Description;
        this.Fork = model.Fork;
        this.Url = model.Url;
        this.ArchiveUrl = model.ArchiveUrl;
    }

    public long? Id { get; }

    public string? NodeId { get; }

    public string? Name { get; }

    public string? FullName { get; }

    public User? Owner { get; }

    public bool? Private { get; }

    public string? HtmlUrl { get; }

    public string? Description { get; }

    public bool? Fork { get; }

    public string? Url { get; }

    public string? ArchiveUrl { get; }
}
