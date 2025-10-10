namespace GithubWebApi.Model;

internal class VerificationModel
{
    [JsonPropertyName("verified")]
    public bool? Verified { get; set; }

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("signature")]
    public string? Signature { get; set; }

    [JsonPropertyName("payload")]
    public string? Payload { get; set; }

    [JsonPropertyName("verified_at")]
    public DateTime? VerifiedAt { get; set; }
}
