namespace GithubWebApi;

public class ContentCommit
{
    internal ContentCommit(ContentCommitModel model)
    {
        this.Content = model.Content.CastModel<Content>();
        this.Commit = model.Commit.CastModel<Commit>();
    }

    public Content? Content { get; }

    public Commit? Commit { get; }
}
