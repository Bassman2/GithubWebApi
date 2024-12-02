namespace GithubWebApi;

[DebuggerDisplay("{Name}")]
public class Commit
{
    internal Commit()
    { }

    internal Commit(CommitModel model)
    {
        this.Sha = model.Sha;
        this.NodeId = NodeId;
        //this.Commit
        this.Url = model.Url; 
        this.HtmlUrl = model.HtmlUrl;
        this.CommentsUrl = model.CommentsUrl;
        this.Author = model.Author; //.Facade<User>();
        this.Committer = model.Committer; //.Facade<User>();
        this.Parents = model.Parents;
    }

    public string? Sha { get; internal init; }

    public string? NodeId { get; internal init; }

    //public CommitCommitModel? Commit { get; internal init;  }

    public string? Url { get; internal init; }

    public string? HtmlUrl { get; internal init; }

    public string? CommentsUrl { get; internal init; }

    public User? Author { get; internal init; }

    public User? Committer { get; internal init; }

    public List<string>? Parents { get; internal init; }
}

