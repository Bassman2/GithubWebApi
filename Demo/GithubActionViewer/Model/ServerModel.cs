namespace GithubActionViewer.Model;

public class ServerModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("key")]
    public string Key { get; set; } = null!;

    [JsonPropertyName("users")]
    public List<UserModel> Users { get; set; } = [];

    [JsonPropertyName("organizations")]
    public List<OrganizationModel> Organizations { get; set; } = [];

    [JsonPropertyName("repositories")]
    public List<RepositoryModel> Repositories { get; set; } = [];

}
