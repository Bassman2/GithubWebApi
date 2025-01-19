namespace GithubWebApi;

public class Content
{
    internal Content(ContentModel model)
    {
        this.Type = model.Type;
        this.Encoding = model.Encoding;
        this.Size = model.Size;
        this.Name = model.Name;
        this.Path = model.Path;
        this.Content_ = model.Content;
        this.Sha = model.Sha;
        this.Url = model.Url;
        this.GitUrl = model.GitUrl;
        this.HtmlUrl = model.HtmlUrl;
        this.DownloadUrl = model.DownloadUrl;
        this.Links = model.Links.CastModel<Links>();
    }

    public string? Type { get; }

    public string? Encoding { get; }

    public long? Size { get; }

    public string? Name { get; }

    public string? Path { get; }

    public string? Content_ { get; }

    public string? Sha { get; }


    public string? Url { get; }


    public string? GitUrl { get; }


    public string? HtmlUrl { get; }


    public string? DownloadUrl { get; }


    public Links? Links { get; }
}
