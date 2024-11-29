namespace GithubWebApi.Service.Model;

internal class RequiredStatusChecksModel
{
    [JsonPropertyName("enforcement_level")]
    public string? EnforcementLevel { get; set; }

    [JsonPropertyName("contexts")]
    public List<string>? Contexts { get; set; }
}
