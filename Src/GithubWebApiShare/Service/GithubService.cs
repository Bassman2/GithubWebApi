namespace GithubWebApi.Service;

// https://docs.github.com/en/rest/pulls/pulls?apiVersion=2022-11-28

internal class GithubService : JsonService
{
    public GithubService(Uri host, string apiKey) : base(host, SourceGenerationContext.Default, new BearerAuthenticator(apiKey))
    { }

    #region error handling

    //protected override async Task ErrorHandlingAsync(HttpResponseMessage response, string memberName, CancellationToken cancellationToken)
    //{
    //    var error = await ReadFromJsonAsync<ErrorRoot>(response, cancellationToken);
    //    WebServiceException.ThrowHttpError(error?.ToString(), response, memberName);
    //}

    #endregion

    #region PullRequest

    public async Task<IEnumerable<PullModel>?> GetPullsAsync(string owner, string repo, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<PullModel>>($"/repos/{owner}/{repo}/pulls", cancellationToken);
        return res;
    }

    public async Task<PullModel?> CreatePullAsync(string owner, string repo, string title, string body, string head, string baseBranch, CancellationToken cancellationToken)
    {
        var req = new CreatePullModel() { Title = title, Body = body, Head = head, Base = baseBranch };
        var res = await PostAsJsonAsync<CreatePullModel, PullModel>($"/repos/{owner}/{repo}/pulls", req, cancellationToken);
        return res;
    }

    public async Task<PullModel?> GetPullAsync(string owner, string repo, int pullNumber, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<PullModel>($"/repos/{owner}/{repo}/pulls/{pullNumber}", cancellationToken);
        return res;
    }

    public async Task<PullModel?> UpdatePullAsync(string owner, string repo, int pullNumber, string title, string body, string state, string baseBranch, CancellationToken cancellationToken)
    {
        var req = new PatchPullModel() { Title = title, Body = body, State = state, Base = baseBranch };
        var res = await PatchAsJsonAsync<PatchPullModel, PullModel>($"/repos/{owner}/{repo}/pulls/{pullNumber}", req, cancellationToken);
        return res;
    }

    //public async Task<PullModel?> UpdatePullBranchAsync(string owner, string repo, int pullNumber, CancellationToken cancellationToken)
    //{
    //    var req = new PatchPullModel() { };
    //    var res = await PatchAsJsonAsync<PatchPullModel, PullModel>($"/repos/{owner}/{repo}/pulls/{pullNumber}", req, cancellationToken);
    //    return res;
    //}

    #endregion
    #region User

    
    public async Task<UserModel?> GetAuthenticatedUserAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<UserModel>($"/user", cancellationToken);
        return res;
    }

    #endregion
}
