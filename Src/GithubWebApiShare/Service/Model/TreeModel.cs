namespace GithubWebApi.Service.Model;

internal class TreeModel
{

    [JsonPropertyName("sha")]
    public string? Sha { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("tree")]
    public List<TreeItemModel>? Trees { get; set; }

    [JsonPropertyName("truncated")]
    public bool Truncated { get; set; }
}
