namespace GithubWebApi.Service.Model;

internal class ProtectionModel
{
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    [JsonPropertyName("required_status_checks")]
    public RequiredStatusChecksModel? RequiredStatusChecks { get; set; }

    public static implicit operator Protection?(ProtectionModel? model)
    {
        return model is null ? null : new Protection()
        {
            Enabled = model.Enabled,
            //RequiredStatusChecks = model.RequiredStatusChecks
        };
    }
}
