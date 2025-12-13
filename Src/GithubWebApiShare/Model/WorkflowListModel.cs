namespace GithubWebApi.Model;

internal class WorkflowListModel
{
    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }

    [JsonPropertyName("workflows")]
    public List<WorkflowModel> Workflows { get; set; } = [];
}
