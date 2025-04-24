namespace GithubWebApi;

/// <summary>
/// Represents a combination of content and its associated commit in a GitHub repository.
/// </summary>
public class ContentCommit
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContentCommit"/> class.
    /// </summary>
    /// <param name="model">The model containing content and commit data.</param>
    internal ContentCommit(ContentCommitModel model)
    {
        this.Content = model.Content.CastModel<Content>();
        this.Commit = model.Commit.CastModel<Commit>();
    }

    /// <summary>
    /// Gets the content associated with the commit.
    /// </summary>
    public Content? Content { get; }

    /// <summary>
    /// Gets the commit associated with the content.
    /// </summary>
    public Commit? Commit { get; }
}
