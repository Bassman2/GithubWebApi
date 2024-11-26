namespace GithubWebApi;

public sealed class Github(Uri host, string apiKey) : IDisposable
{
    private GithubService? service = new(host, apiKey);

    public void Dispose()
    {
        if (this.service != null)
        {
            this.service.Dispose();
            this.service = null;
        }
        GC.SuppressFinalize(this);
    }

    #region Pull Request

    public async Task<IEnumerable<PullRequest>?> GetPullsAsync(string owner, string repo, CancellationToken cancellationToken)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetPullsAsync(owner, repo, cancellationToken);
        return res?.Select(p => new PullRequest(p)).ToList();
    }

    public async Task<PullRequest?> CreatePullAsync(string owner, string repo, string title, string body, string head, string baseBranch, CancellationToken cancellationToken)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.CreatePullAsync(owner, repo, title, body, head, baseBranch, cancellationToken);
        return res is not null ? new PullRequest(res) : null;
    }

    public async Task<PullRequest?> GetPullAsync(string owner, string repo, int pullNumber, CancellationToken cancellationToken)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetPullAsync(owner, repo, pullNumber, cancellationToken);
        return res is not null ? new PullRequest(res) : null; 
    }

    public async Task<PullRequest?> UpdatePullAsync(string owner, string repo, int pullNumber, string title, string body, string state, string baseBranch, CancellationToken cancellationToken)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.UpdatePullAsync(owner, repo, pullNumber, title, body, state, baseBranch, cancellationToken);
        return res is not null ? new PullRequest(res) : null; 
    }

    #endregion

    #region User

    public async Task<User?> GetAuthenticatedUserAsync(CancellationToken cancellationToken)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetAuthenticatedUserAsync(cancellationToken);
        return res is not null ? new User(res) : null;
    }

    #endregion
}
