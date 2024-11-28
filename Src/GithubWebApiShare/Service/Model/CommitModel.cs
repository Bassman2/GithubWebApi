namespace GithubWebApi.Service.Model;

public class CommitModel
{

    [JsonPropertyName("sha")]
    public string? Sha { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

}
