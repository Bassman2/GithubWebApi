namespace GithubWebApi;

public class PullRequest
{
    internal PullRequest(PullModel model)
    {
        this.Url = model.Url;
        this.Id = model.Id;
    }

    public string? Url { get; }

    [JsonPropertyName("folder")]
    public int? Id { get; }
}
