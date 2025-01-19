namespace GithubWebApi.Service.Model;

internal class ContentCreateModel
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("sha")]
    public string? Sha { get; set; }

    [JsonPropertyName("branch")]
    public string? Branch{ get; set; }

    [JsonPropertyName("committer")]
    public UserModel? Committer { get; set; }

    [JsonPropertyName("author")]
    public UserModel? Author { get; set; }
}
