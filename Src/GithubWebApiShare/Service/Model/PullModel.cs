namespace GithubWebApi.Service.Model;

internal class PullModel
{

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("id")]
    public int? Id { get; set; }
}
