namespace GithubWebApi.Model;

internal class CodespaceModel
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; set; }
}
