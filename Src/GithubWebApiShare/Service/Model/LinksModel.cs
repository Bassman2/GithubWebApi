namespace GithubWebApi.Service.Model;

internal class LinksModel
{
    [JsonPropertyName("git")]
    public string? Git { get; set; }

    [JsonPropertyName("self")]
    public string? Self { get; set; }

    [JsonPropertyName("html")]
    public string? Html { get; set; }
}
