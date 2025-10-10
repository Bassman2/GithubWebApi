namespace GithubWebApi.Model;

internal class ContentCommitModel
{
    [JsonPropertyName("type")]
    public ContentModel? Content { get; set; }

    [JsonPropertyName("commit")]
    public CommitModel? Commit { get; set; }
}
