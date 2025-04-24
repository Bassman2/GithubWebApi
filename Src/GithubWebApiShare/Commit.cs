namespace GithubWebApi;

/// <summary>
/// Represents a commit in a GitHub repository.
/// </summary>
[DebuggerDisplay("{Name}")]
public class Commit
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Commit"/> class.
    /// </summary>
    /// <param name="model">The model containing commit data.</param>
    internal Commit(CommitModel model)
    {
        this.Sha = model.Sha;
        this.NodeId = model.NodeId;
        //this.Commit
        this.Url = model.Url;
        this.HtmlUrl = model.HtmlUrl;
        this.CommentsUrl = model.CommentsUrl;
        this.Author = model.Author.CastModel<User>();
        this.Committer = model.Committer.CastModel<User>();
        //this.Parents = model.Parents;
    }

    /// <summary>
    /// Gets the SHA hash of the commit.
    /// </summary>
    public string? Sha { get; }

    /// <summary>
    /// Gets the Node ID of the commit.
    /// </summary>
    public string? NodeId { get; }

    //public CommitCommitModel? Commit { get; internal init;  }

    /// <summary>
    /// Gets the URL of the commit.
    /// </summary>
    public string? Url { get; }

    /// <summary>
    /// Gets the HTML URL of the commit.
    /// </summary>
    public string? HtmlUrl { get; }

    /// <summary>
    /// Gets the URL for comments associated with the commit.
    /// </summary>
    public string? CommentsUrl { get; }

    /// <summary>
    /// Gets the author of the commit.
    /// </summary>
    public User? Author { get; }

    /// <summary>
    /// Gets the committer of the commit.
    /// </summary>
    public User? Committer { get; }

    /// <summary>
    /// Gets the list of parent commit SHAs.
    /// </summary>
    public List<string>? Parents { get; }
}

