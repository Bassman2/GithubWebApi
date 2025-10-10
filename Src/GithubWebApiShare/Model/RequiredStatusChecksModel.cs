namespace GithubWebApi.Model;

internal class RequiredStatusChecksModel
{
    [JsonPropertyName("enforcement_level")]
    public string? EnforcementLevel { get; set; }

    [JsonPropertyName("contexts")]
    public List<string>? Contexts { get; set; }

    [JsonPropertyName("checks")]
    public List<string>? Checks { get; set; }
}
