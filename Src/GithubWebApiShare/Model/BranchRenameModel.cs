namespace GithubWebApi.Model;

internal class BranchRenameModel
{
    [JsonPropertyName("new_name")]
    public string? NewName { get; set; }
}
