namespace GithubActionViewer.Model;

public class UserModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("key")]
    public string Key { get; set; } = null!;
}
