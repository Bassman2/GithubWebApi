namespace GithubWebApi.Model;

internal class ContentModel
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("encoding")]
    public string? Encoding { get; set; }

    [JsonPropertyName("size")]
    public long? Size { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("path")]
    public string? Path { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("sha")]
    public string? Sha { get; set; }


    [JsonPropertyName("url")]
    public string? Url { get; set; }


    [JsonPropertyName("git_url")]
    public string? GitUrl { get; set; }


    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; set; }


    [JsonPropertyName("download_url")]
    public string? DownloadUrl { get; set; }


    [JsonPropertyName("_links")]
    public LinksModel? Links { get; set; }
}
