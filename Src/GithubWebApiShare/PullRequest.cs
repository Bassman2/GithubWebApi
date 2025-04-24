namespace GithubWebApi;

/// <summary>
/// Represents a pull request in a GitHub repository.
/// </summary>
public class PullRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PullRequest"/> class.
    /// </summary>
    /// <param name="model">The model containing pull request data.</param>
    internal PullRequest(PullModel model)
    {
        this.Url = model.Url;
        this.Id = model.Id;
    }

    /// <summary>
    /// Gets the URL of the pull request.
    /// </summary>
    public string? Url { get; }

    /// <summary>
    /// Gets the unique identifier of the pull request.
    /// </summary>
    public int? Id { get; }
}
