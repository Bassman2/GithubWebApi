namespace GithubActionViewer.Model;

public class RepositoryModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("key")]
    public string Key { get; set; } = null!;

    [JsonPropertyName("owner")]
    public string Owner { get; set; } = null!;

    [JsonPropertyName("branches")]
    public List<string>? Branches { get; set; }
}
