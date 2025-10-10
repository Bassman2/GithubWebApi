namespace GithubWebApi.Model;

internal class MetaModel
{
    [JsonPropertyName("installed_version")]
    public string? InstalledVersion { get; set; }
}


// {"verifiable_password_authentication":true,"installed_version":"3.14.11"}
