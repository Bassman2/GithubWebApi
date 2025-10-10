namespace GithubWebApi.Service.Model;

internal class PatchPullModel

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

// {"title":"new title","body":"updated body","state":"open","base":"master"}'