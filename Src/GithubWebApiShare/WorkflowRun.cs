namespace GithubWebApi;

/// <summary>
/// Represents a GitHub Actions workflow run, including metadata such as status, event, and associated users.
/// </summary>
public class WorkflowRun
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WorkflowRun"/> class from a <see cref="WorkflowRunModel"/>.
    /// </summary>
    /// <param name="model">The model containing workflow run data from the GitHub API.</param>
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

    /// <summary>
    /// Gets the unique identifier of the workflow run.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Gets the node ID of the workflow run (GraphQL global node ID).
    /// </summary>
    public string? NodeId { get; }

    /// <summary>
    /// Gets the name of the workflow run.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Gets the unique identifier of the associated check suite.
    /// </summary>
    public long CheckSuiteId { get; }

    /// <summary>
    /// Gets the node ID of the associated check suite.
    /// </summary>
    public string? CheckSuiteNodeId { get; }

    /// <summary>
    /// Gets the name of the branch the workflow run is associated with.
    /// </summary>
    public string? HeadBranch { get; }

    /// <summary>
    /// Gets the commit SHA the workflow run is associated with.
    /// </summary>
    public string? HeadSha { get; }

    /// <summary>
    /// Gets the file path of the workflow definition.
    /// </summary>
    public string? Path { get; }

    /// <summary>
    /// Gets the run number for this workflow run.
    /// </summary>
    public int RunNumber { get; }

    /// <summary>
    /// Gets the event that triggered the workflow run (e.g., "push", "pull_request").
    /// </summary>
    public string? Event { get; }

    /// <summary>
    /// Gets the display title of the workflow run.
    /// </summary>
    public string? DisplayTitle { get; }

    /// <summary>
    /// Gets the current status of the workflow run (e.g., "queued", "in_progress", "completed").
    /// </summary>
    public string? Status { get; }

    /// <summary>
    /// Gets the conclusion of the workflow run (e.g., "success", "failure", "cancelled").
    /// </summary>
    public string? Conclusion { get; }

    /// <summary>
    /// Gets the unique identifier of the workflow.
    /// </summary>
    public int WorkflowId { get; }

    /// <summary>
    /// Gets the API URL for the workflow run.
    /// </summary>
    public string? Url { get; }

    /// <summary>
    /// Gets the HTML URL for the workflow run on GitHub.
    /// </summary>
    public string? HtmlUrl { get; }

    /// <summary>
    /// Gets the list of pull requests associated with this workflow run.
    /// </summary>
    public List<string?> PullRequests { get; } = [];

    /// <summary>
    /// Gets the date and time when the workflow run was created.
    /// </summary>
    public DateTime? CreatedAt { get; }

    /// <summary>
    /// Gets the date and time when the workflow run was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; }

    /// <summary>
    /// Gets the user who initiated the workflow run.
    /// </summary>
    public User? Actor { get; }

    /// <summary>
    /// Gets the attempt number for this workflow run (for re-runs).
    /// </summary>
    public int RunAttempt { get; }

    /// <summary>
    /// Gets the date and time when the workflow run started.
    /// </summary>
    public DateTime? RunStartedAt { get; }

    /// <summary>
    /// Gets the user who triggered the workflow run.
    /// </summary>
    public User? TriggeringActor { get; }
}
