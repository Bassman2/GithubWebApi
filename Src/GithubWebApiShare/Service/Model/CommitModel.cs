namespace GithubWebApi.Service.Model;

internal class CommitModel
{

    [JsonPropertyName("sha")]
    public string? Sha { get; set; }

    [JsonPropertyName("node_id")]
    public string? NodeId { get; set; }

    [JsonPropertyName("commit")]
    public CommitCommitModel? Commit { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; set; }

    [JsonPropertyName("comments_url")]
    public string? CommentsUrl { get; set; }

    [JsonPropertyName("author")]
    public UserModel? Author { get; set; }

    [JsonPropertyName("committer")]
    public UserModel? Committer { get; set; }

    //[JsonPropertyName("parents")]
    //public List<string>? Parents { get; set; }

}
