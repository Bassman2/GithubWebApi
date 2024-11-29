namespace GithubWebApi.Service.Model;

internal class ProtectionModel
{
    [JsonPropertyName("required_status_checks")]
    public RequiredStatusChecksModel? RequiredStatusChecks { get; set; }
}
