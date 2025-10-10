namespace GithubWebApi.Model;

internal class ReleaseModel
{
    [JsonPropertyName("ref")]
    public string? Ref { get; set; }
}
