namespace GithubWebApi.Model;

internal class WorkflowRunModel
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("node_id")]
    public string? NodeId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("check_suite_id")]
    public long CheckSuiteId { get; set; }

    [JsonPropertyName("check_suite_node_id")]
    public string? CheckSuiteNodeId { get; set; }

    [JsonPropertyName("head_branch")]
    public string? HeadBranch { get; set; }

    [JsonPropertyName("head_sha")]
    public string? HeadSha { get; set; }

    [JsonPropertyName("path")]
    public string? Path { get; set; }

    [JsonPropertyName("run_number")]
    public int RunNumber { get; set; }

    [JsonPropertyName("event")]
    public string? Event { get; set; }

    [JsonPropertyName("display_title")]
    public string? DisplayTitle { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("conclusion")]
    public string? Conclusion { get; set; }

    [JsonPropertyName("workflow_id")]
    public int WorkflowId { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; set; }

    [JsonPropertyName("pull_requests")]
    public List<string?> PullRequests { get; set; } = [];

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
       
    [JsonPropertyName("actor")]
    public UserModel? Actor { get; set; }

    [JsonPropertyName("run_attempt")]
    public int RunAttempt { get; set; }

    [JsonPropertyName("run_started_at")]
    public DateTime? RunStartedAt { get; set; }

    [JsonPropertyName("triggering_actor")]
    public UserModel? TriggeringActor { get; set; }
}
