namespace GithubWebApi.Model;

internal class CodespaceListModel
{

    [JsonPropertyName("total_count")]
    public int? TotalCount { get; set; }

    [JsonPropertyName("codespaces")]
    public List<CodespaceListModel>? Codespaces { get; set; }
}
