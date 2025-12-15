namespace GithubWebApi;

/// <summary>
/// Represents a GitHub Actions workflow within a repository.
/// </summary>
public class Workflow
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Workflow"/> class from a <see cref="WorkflowModel"/>.
    /// </summary>
    /// <param name="model">The model containing workflow data from the GitHub API.</param>
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

    /// <summary>
    /// Gets the unique identifier of the workflow.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the node ID of the workflow (GraphQL global node ID).
    /// </summary>
    public string? NodeId { get;}

    /// <summary>
    /// Gets the name of the workflow.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Gets the file path of the workflow definition in the repository.
    /// </summary>
    public string? Path { get; }

    /// <summary>
    /// Gets the state of the workflow (e.g., "active" or "disabled").
    /// </summary>
    public string? State { get; }

    /// <summary>
    /// Gets the date and time when the workflow was created.
    /// </summary>
    public DateTime? CreatedAt { get; }

    /// <summary>
    /// Gets the date and time when the workflow was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; }

    /// <summary>
    /// Gets the API URL for the workflow.
    /// </summary>
    public string? Url { get; }

    /// <summary>
    /// Gets the HTML URL for the workflow on GitHub.
    /// </summary>
    public string? HtmlUrl { get; }

    /// <summary>
    /// Gets the URL for the workflow badge image.
    /// </summary>
    public string? BadgeUrl { get; }
}
