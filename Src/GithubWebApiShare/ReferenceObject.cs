namespace GithubWebApi;

public class ReferenceObject
{
    internal ReferenceObject(ReferenceObjectModel model)
    {
        Sha = model.Sha;
        Type = model.Type;
        Url = model.Url;
    }

    public string? Sha { get; }


    public string? Type { get; }


    public string? Url { get; }

}
