namespace GithubWebApi;

public class Links
{
    internal Links(LinksModel model)
    {
        this.Self = model.Self;
        this.Html = model.Html;
    }

    public string? Self { get; }

    public string? Html { get; }
}
