namespace GithubWebApi.Service.Model;

internal class ProtectionModel
{
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    [JsonPropertyName("required_status_checks")]
    public RequiredStatusChecksModel? RequiredStatusChecks { get; set; }
}
