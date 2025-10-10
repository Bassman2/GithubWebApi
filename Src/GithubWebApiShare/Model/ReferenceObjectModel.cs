namespace GithubWebApi.Model;

internal class ReferenceObjectModel
{
    [JsonPropertyName("sha")]
    public string? Sha { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}
