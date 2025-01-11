namespace GithubWebApi.Service.Model;

internal class PullCreateModel
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("head")]
    public string? Head { get; set; }

    [JsonPropertyName("base")]
    public string? Base { get; set; }
}

