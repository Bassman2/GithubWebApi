namespace GithubWebApi.Service.Model;

internal class ReleaseModel
{
    [JsonPropertyName("ref")]
    public string? Ref { get; set; }
}
