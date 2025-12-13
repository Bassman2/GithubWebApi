namespace GithubWebApi;

public class Workflow
{
    internal Workflow(WorkflowModel model)
    {
        this.Id = model.Id;
        this.NodeId = model.NodeId;
        this.Name = model.Name;
        this.Path = model.Path;
        this.State = model.State;
        this.CreatedAt = model.CreatedAt;
        this.UpdatedAt = model.UpdatedAt;
        this.Url = model.Url;
        this.HtmlUrl = model.HtmlUrl;
        this.BadgeUrl = model.BadgeUrl;
    }

    public int Id { get; }
    public string? NodeId { get;}
    public string? Name { get; }
    public string? Path { get; }
    public string? State { get; }
    public DateTime? CreatedAt { get; }
    public DateTime? UpdatedAt { get; }
    public string? Url { get; }
    public string? HtmlUrl { get; }
    public string? BadgeUrl { get; }





}
