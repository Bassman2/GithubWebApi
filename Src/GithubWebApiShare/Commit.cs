namespace GithubWebApi;

[DebuggerDisplay("{Name}")]
public class Commit
{
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

    public string? Sha { get; }

    public string? NodeId { get; }

    //public CommitCommitModel? Commit { get; internal init;  }

    public string? Url { get; }

    public string? HtmlUrl { get; }

    public string? CommentsUrl { get; }

    public User? Author { get; }

    public User? Committer { get; }

    public List<string>? Parents { get; }
}

