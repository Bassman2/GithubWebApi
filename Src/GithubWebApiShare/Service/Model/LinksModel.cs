namespace GithubWebApi.Service.Model;

internal class LinksModel
{
    [JsonPropertyName("self")]
    public string? Self { get; set; }

    [JsonPropertyName("html")]
    public string? Html { get; set; }
}
