namespace GithubWebApi.Model;

internal class CommitCommitModel
{
    [JsonPropertyName("author")]
    public UserModel? Author { get; set; }

    [JsonPropertyName("committer")]
    public UserModel? Committer { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("tree")]
    public TagModel? Tree { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("comment_count")]
    public int? CommentCount { get; set; }

    [JsonPropertyName("verification")]
    public VerificationModel? Verification { get; set; }
}
