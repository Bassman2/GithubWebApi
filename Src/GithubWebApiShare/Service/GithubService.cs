namespace GithubWebApi.Service;

// https://docs.github.com/en/rest/pulls/pulls?apiVersion=2022-11-28

internal partial class GithubService : JsonService
{
    private const string defHost = "https://api.github.com";
    protected override string? AuthenticationTestUrl => "/user";

    public GithubService(string apiKey, string appName) : this(new Uri(defHost), apiKey, appName)
    { }

    public GithubService(Uri host, string apiKey, string appName) : base(host, SourceGenerationContext.Default, new BearerAuthenticator(apiKey))
    {
        client!.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
        client!.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
        client!.DefaultRequestHeaders.Add("User-Agent", appName);
    }

    #region error handling

    //protected override async Task ErrorHandlingAsync(HttpResponseMessage response, string memberName, CancellationToken cancellationToken)
    //{
    //    var error = await ReadFromJsonAsync<ErrorRoot>(response, cancellationToken);
    //    WebServiceException.ThrowHttpError(error?.ToString(), response, memberName);
    //}

    #endregion

    #region Branches

    public IAsyncEnumerable<BranchModel> GetBranchesAsync(string owner, string repo, CancellationToken cancellationToken)
    {
        var res = GetFromJsonYieldAsync<BranchModel>($"/repos/{owner}/{repo}/branches", cancellationToken);
        return res;
    }

    public async Task<BranchModel?> GetBranchAsync(string owner, string repo, string branch, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<BranchModel>($"/repos/{owner}/{repo}/branches/{branch}", cancellationToken);
        return res;
    }

    public async Task<BranchModel?> RenameBranchAsync(string owner, string repo, string branch, string newName, CancellationToken cancellationToken)
    {
        var req = new BranchRenameModel() { NewName = newName };
        var res = await PostAsJsonAsync<BranchRenameModel, BranchModel>($"/repos/{owner}/{repo}/branches/{branch}/rename", req, cancellationToken);
        return res;
    }

    // from main branch
    public async Task<BranchModel?> CreateBranchAsync(string owner, string repo, string newBranchName, CancellationToken cancellationToken)
    {
        RefModel? _ref = await GetFromJsonAsync<RefModel>($"/repos/{owner}/{repo}/git/refs/heads", cancellationToken);

        var req = new RefModel() { Ref = $"refs/heads/{newBranchName}", Sha = _ref!.Sha };
        var res = await PostAsJsonAsync<RefModel, BranchModel>($"/repos/{owner}/{repo}/git/refs/heads", req, cancellationToken);
        return res;
    }

    /*
    Response => {
[
{
    "ref": "refs/heads/<already present branch name for ref>",
    "node_id": "jkdhoOIHOO65464edg66464GNLNLnlnnlnlna==",
    "url": " https://api.github.com/repos/<your login name>/<Your Repository Name>/git/refs/heads/<already present branch name for ref>",
    "object": {
        "sha": "guDSGss85s1KBih546465kkbNNKKbkSGyjes56",
        "type": "commit",
        "url": " https://api.github.com/repos/<your login name>/<Your Repository Name>/git/commits/guDSGss85s1KBih546465kkbNNKKbkSGyjes56"
    }
}
]
}
    */

    // from other branch
    public async Task<BranchModel?> CreateBranchAsync(string owner, string repo, string branch, string newBranchName, CancellationToken cancellationToken)
    {
        RefModel? _ref = await GetFromJsonAsync<RefModel>($"/repos/{owner}/{repo}/git/refs/heads/{branch}", cancellationToken);

        var req = new RefModel() { Ref = $"refs/heads/{newBranchName}", Sha = _ref!.Sha };
        var res = await PostAsJsonAsync<RefModel, BranchModel>($"/repos/{owner}/{repo}/git/refs/heads", req, cancellationToken);
        return res;
    }

    #endregion

    #region Codespaces

    public async Task<IEnumerable<BranchModel>?> GetCodespacesAsync(string owner, string repo, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<BranchModel>>($"/repos/{owner}/{repo}/codespaces", cancellationToken);
        return res;
    }

    #endregion
    
    #region PullRequest

    public async Task<IEnumerable<PullModel>?> GetPullsAsync(string owner, string repo, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<PullModel>>($"/repos/{owner}/{repo}/pulls", cancellationToken);
        return res;
    }

    public async Task<PullModel?> CreatePullAsync(string owner, string repo, string title, string body, string head, string baseBranch, CancellationToken cancellationToken)
    {
        var req = new PullCreateModel() { Title = title, Body = body, Head = head, Base = baseBranch };
        var res = await PostAsJsonAsync<PullCreateModel, PullModel>($"/repos/{owner}/{repo}/pulls", req, cancellationToken);
        return res;
    }

    public async Task<PullModel?> GetPullAsync(string owner, string repo, int pullNumber, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<PullModel>($"/repos/{owner}/{repo}/pulls/{pullNumber}", cancellationToken);
        return res;
    }

    public async Task<PullModel?> UpdatePullAsync(string owner, string repo, int pullNumber, string title, string body, string state, string baseBranch, CancellationToken cancellationToken)
    {
        var req = new PullPatchModel() { Title = title, Body = body, State = state, Base = baseBranch };
        var res = await PatchAsJsonAsync<PullPatchModel, PullModel>($"/repos/{owner}/{repo}/pulls/{pullNumber}", req, cancellationToken);
        return res;
    }

    //public async Task<PullModel?> UpdatePullBranchAsync(string owner, string repo, int pullNumber, CancellationToken cancellationToken)
    //{
    //    var req = new PullPatchModel() { };
    //    var res = await PatchAsJsonAsync<PullPatchModel, PullModel>($"/repos/{owner}/{repo}/pulls/{pullNumber}", req, cancellationToken);
    //    return res;
    //}

    #endregion

    #region Repositories

    public IAsyncEnumerable<RepositoryModel> GetOrganizationRepositoriesAsync(string org, CancellationToken cancellationToken)
    {
        var res = GetFromJsonYieldAsync<RepositoryModel>($"/orgs/{org}/repos", cancellationToken);
        return res;
    }  

    public IAsyncEnumerable<RepositoryModel> GetAuthenticatedUserRepositoriesAsync(CancellationToken cancellationToken)
    {
        var res = GetFromJsonYieldAsync<RepositoryModel>($"/user/repos", cancellationToken);
        return res;
    }

    public IAsyncEnumerable<RepositoryModel> GetUserRepositoriesAsync(string user, CancellationToken cancellationToken)
    {
        var res = GetFromJsonYieldAsync<RepositoryModel>($"/users/{user}/repos", cancellationToken);
        return res;
    }

    public IAsyncEnumerable<RepositoryModel> GetPublicRepositoriesAsync(int since, CancellationToken cancellationToken)
    {
        var res = GetFromJsonYieldAsync<RepositoryModel>($"/repositories?since={since}", cancellationToken);
        return res;
    }

    public async Task<RepositoryModel?> GetRepositoryAsync(string owner, string repo, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<RepositoryModel>($"/repos/{owner}/{repo}", cancellationToken);
        return res;
    }


    public async Task<IEnumerable<TagModel>?> GetRepositoryTagsAsync(string org, string repo,CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<TagModel>>($"/repos/{org}/{repo}/tags", cancellationToken);
        return res;
    }

    public async Task<IEnumerable<TeamModel>?> GetRepositoryTeamsAsync(string org, string repo, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<TeamModel>>($"/repos/{org}/{repo}/teams", cancellationToken);
        return res;
    }

    #endregion

    #region Tags

    #endregion

    #region User

    public async Task<UserModel?> GetAuthenticatedUserAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<UserModel>("/user", cancellationToken);
        return res;
    }

    public async Task<UserModel?> GetUserAsync(string username, CancellationToken cancellationToken)
    { 
        ArgumentException.ThrowIfNullOrWhiteSpace(username, nameof(username));

        var res = await GetFromJsonAsync<UserModel>($"/users/{username}", cancellationToken);
        return res;
    }

    public async Task<UserModel?> GetUserAsync(long id, CancellationToken cancellationToken)
    {

        var res = await GetFromJsonAsync<UserModel>($"/user/{id}", cancellationToken);
        return res;
    }

    #endregion



    public async Task<BranchModel?> GetHeadRevisionAsync(string owner, string repo, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<BranchModel>($"/repos/{owner}/{repo}/git/refs/heads", cancellationToken);
        return res;
    }

    /*

    [
  {
    "ref": "refs/heads/AED2-8320_display_username_after_signin",
    "node_id": "MDM6UmVmNjg1MzpyZWZzL2hlYWRzL0FFRDItODMyMF9kaXNwbGF5X3VzZXJuYW1lX2FmdGVyX3NpZ25pbg==",
    "url": "https://gitext.elektrobitautomotive.com/api/v3/repos/EB-GUIDE-Speech/aacs-app/git/refs/heads/AED2-8320_display_username_after_signin",
    "object": {
      "sha": "d480f41ab6c419d01e581ff71076db7aa330d332",
      "type": "commit",
      "url": "https://gitext.elektrobitautomotive.com/api/v3/repos/EB-GUIDE-Speech/aacs-app/git/commits/d480f41ab6c419d01e581ff71076db7aa330d332"
    }
  },
  {
    "ref": "refs/heads/AED2-11937_POC_thinking_4.2",
    "node_id": "MDM6UmVmNjg1MzpyZWZzL2hlYWRzL0FFRDItMTE5MzdfUE9DX3RoaW5raW5nXzQuMg==",
    "url": "https://gitext.elektrobitautomotive.com/api/v3/repos/EB-GUIDE-Speech/aacs-app/git/refs/heads/AED2-11937_POC_thinking_4.2",
    "object": {
      "sha": "873f1a48b123c06a0e112a1141e6c7245ef023dd",
      "type": "commit",
      "url": "https://gitext.elektrobitautomotive.com/api/v3/repos/EB-GUIDE-Speech/aacs-app/git/commits/873f1a48b123c06a0e112a1141e6c7245ef023dd"
    }
  },
  {
    "ref": "refs/heads/AED2-16547_VC_state_on_service_start",
    "node_id": "MDM6UmVmNjg1MzpyZWZzL2hlYWRzL0FFRDItMTY1NDdfVkNfc3RhdGVfb25fc2VydmljZV9zdGFydA==",
    "url": "https://gitext.elektrobitautomotive.com/api/v3/repos/EB-GUIDE-Speech/aacs-app/git/refs/heads/AED2-16547_VC_state_on_service_start",
    "object": {
      "sha": "f7f599bce018a05a5eeadec1d2b4ab1c9241e2f1",
      "type": "commit",
      "url": "https://gitext.elektrobitautomotive.com/api/v3/repos/EB-GUIDE-Speech/aacs-app/git/commits/f7f599bce018a05a5eeadec1d2b4ab1c9241e2f1"
    }
  },
  {
    "ref": "refs/heads/asterix2-4.2",
    "node_id": "MDM6UmVmNjg1MzpyZWZzL2hlYWRzL2FzdGVyaXgyLTQuMg==",
    "url": "https://gitext.elektrobitautomotive.com/api/v3/repos/EB-GUIDE-Speech/aacs-app/git/refs/heads/asterix2-4.2",
    "object": {
      "sha": "77707bfb4dc9f59d1cdaa8960fc568be3117850c",
      "type": "commit",
      "url": "https://gitext.elektrobitautomotive.com/api/v3/repos/EB-GUIDE-Speech/aacs-app/git/commits/77707bfb4dc9f59d1cdaa8960fc568be3117850c"
    }
  },


    */


    #region Private

    private async IAsyncEnumerable<T> GetFromJsonYieldAsync<T>(string? requestUri, [EnumeratorCancellation] CancellationToken cancellationToken, [CallerMemberName] string memberName = "") where T : class
    {
        ArgumentRequestUriException.ThrowIfNullOrWhiteSpace(requestUri, nameof(requestUri));
        WebServiceException.ThrowIfNullOrNotConnected(this);

        while (requestUri != null)
        {
            using HttpResponseMessage response = await client!.GetAsync(requestUri, cancellationToken);
            string str = await response.Content.ReadAsStringAsync(cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                await ErrorHandlingAsync(response, memberName, cancellationToken);
            }

            var res = await ReadFromJsonAsync<IEnumerable<T>?>(response, cancellationToken);
            if (res != null)
            {
                foreach (var item in res)
                {
                    yield return item;
                }
            }
            requestUri = GithubService.NextLink(response);
        }
    }

    [GeneratedRegex(@"\<([^\<]*)\>;\srel=.next.", RegexOptions.Singleline)]
    private static partial Regex LinkRegex();

    private static string? NextLink(HttpResponseMessage response)
    {
        if (response.Headers.TryGetValues("link", out var header))
        {
            var links = header.First();
            Match match = LinkRegex().Match(links);
            if (match.Success)
            {
                string next = match.Groups[1].Value;
                return next;
            }
        }
        return null;
    }


    #endregion
}
