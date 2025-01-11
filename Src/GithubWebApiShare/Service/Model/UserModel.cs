namespace GithubWebApi.Service.Model;

internal class UserModel
{
    [JsonPropertyName("login")]
    public string? Login { get; set; }

    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("node_id")]
    public string? NodeId { get; set; }

    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("site_admin")]
    public bool? SiteAdmin { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("company")]
    public string? Company { get; set; }

    [JsonPropertyName("location")]
    public string? Location { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }
}
