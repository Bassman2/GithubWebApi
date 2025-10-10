namespace GithubWebApi.Model;

[DebuggerDisplay("{Ref} {Object.Type}")]
internal class ReferenceModel
{
    [JsonPropertyName("ref")]
    public string? Ref { get; set; }

    [JsonPropertyName("node_id")]
    public string? NodeId { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("object")]
    public ReferenceObjectModel? Object { get; set; }
}
