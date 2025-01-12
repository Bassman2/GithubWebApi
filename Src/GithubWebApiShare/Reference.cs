namespace GithubWebApi;

public class Reference
{
    internal Reference(ReferenceModel model)
    {
        Ref = model.Ref;
        NodeId = model.NodeId;
        Url = model.Url;
        Object = model.Object.CastModel<ReferenceObject>();
    }

    public string? Ref { get; }

    public string? NodeId { get; }

    public string? Url { get; }

    public ReferenceObject? Object { get; }
}
