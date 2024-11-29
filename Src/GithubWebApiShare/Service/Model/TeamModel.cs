namespace GithubWebApi.Service.Model;

internal class TeamModel
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("node_id")]
    public string? NodeId { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("privacy")]
    public string? Privacy { get; set; }

    [JsonPropertyName("notification_setting")]
    public string? NotificationSetting { get; set; }

    [JsonPropertyName("permission")]
    public string? Permission { get; set; }

    [JsonPropertyName("members_url")]
    public string? MembersUrl { get; set; }

    [JsonPropertyName("repositories_url")]
    public string? RepositoriesUrl { get; set; }

    [JsonPropertyName("parent")]
    public string? Parent { get; set; }

}
