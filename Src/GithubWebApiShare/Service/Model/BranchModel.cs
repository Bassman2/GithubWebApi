namespace GithubWebApi.Service.Model;

internal class BranchModel
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("commit")]
    public CommitModel? Commit { get; set; }

    [JsonPropertyName("_links")]
    public LinksModel? Links { get; set; }

    [JsonPropertyName("protected")]
    public bool? Protected { get; set; }

    [JsonPropertyName("protection")]
    public ProtectionModel? Protection { get; set; }

    [JsonPropertyName("protection_url")]
    public string? ProtectionUrl { get; set; }
}
