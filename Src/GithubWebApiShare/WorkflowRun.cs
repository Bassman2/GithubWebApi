namespace GithubWebApi;

public class WorkflowRun
{
    internal WorkflowRun(WorkflowRunModel model)
    {
        this.Id = model.Id;
        this.NodeId = model.NodeId;
        this.Name = model.Name;
        this.CheckSuiteId = model.CheckSuiteId;
        this.CheckSuiteNodeId = model.CheckSuiteNodeId;
        this.HeadBranch = model.HeadBranch;
        this.HeadSha = model.HeadSha;
        this.Path = model.Path;
        this.RunNumber = model.RunNumber;
        this.Event = model.Event;
        this.DisplayTitle = model.DisplayTitle;
        this.Status = model.Status;
        this.Conclusion = model.Conclusion;
        this.WorkflowId = model.WorkflowId;
        this.Url = model.Url;
        this.HtmlUrl = model.HtmlUrl;
        this.PullRequests = model.PullRequests;
        this.CreatedAt = model.CreatedAt;
        this.UpdatedAt = model.UpdatedAt;
        this.Actor = model.Actor.CastModel<User>(); 
        this.RunAttempt = model.RunAttempt;
        this.RunStartedAt = model.RunStartedAt;
        this.TriggeringActor = model.TriggeringActor.CastModel<User>();
    }

    public long Id { get; }

    public string? NodeId { get; }

    public string? Name { get; }

    public long CheckSuiteId { get; }

    public string? CheckSuiteNodeId { get; }

    public string? HeadBranch { get; }

    public string? HeadSha { get; }

    public string? Path { get; }

    public int RunNumber { get; }

    public string? Event { get; }

    public string? DisplayTitle { get; }

    public string? Status { get; }

    public string? Conclusion { get; }

    public int WorkflowId { get; }

    public string? Url { get; }

    public string? HtmlUrl { get; }

    public List<string?> PullRequests { get; } = [];

    public DateTime? CreatedAt { get; }

    public DateTime? UpdatedAt { get; }

    public User? Actor { get; }

    public int RunAttempt { get; }

    public DateTime? RunStartedAt { get; }

    public User? TriggeringActor { get; }
}
