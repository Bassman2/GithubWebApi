namespace GithubWebApi.Model;

internal class WorkflowRunListModel
{
    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }

    [JsonPropertyName("workflow_runs")]
    public List<WorkflowRunModel> WorkflowRuns { get; set; } = [];
}
