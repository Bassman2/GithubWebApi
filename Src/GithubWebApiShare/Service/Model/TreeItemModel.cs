namespace GithubWebApi.Service.Model;

internal class TreeItemModel
{
    [JsonPropertyName("path")]
    public string? Path { get; set; }

    [JsonPropertyName("mode")]
    public string? Mode { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    // not for create
    [JsonPropertyName("size")]
    public long? Size { get; set; }

    [JsonPropertyName("sha")]
    public string? Sha { get; set; }

    // not for create
    [JsonPropertyName("url")]
    public string? Url { get; set; }
}
