namespace GithubWebApi.Service.Model;

internal class TagModel
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("commit")]
    public CommitModel? Commit { get; set; }

    [JsonPropertyName("zipball_url")]
    public string? ZipballUrl { get; set; }

    [JsonPropertyName("tarball_url")]
    public string? TarballUrl { get; set; }

    [JsonPropertyName("node_id")]
    public string? NodeId { get; set; }
}
