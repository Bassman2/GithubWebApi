namespace GithubWebApi;

public class Release
{
    internal Release(ReleaseModel model)
    {
        this.Ref = model.Ref;
    }

    public string? Ref { get; }
}
