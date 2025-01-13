namespace GithubWebApi.Service.Model;

internal class PullPatchModel
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("base")]
    public string? Base { get; set; }
}

