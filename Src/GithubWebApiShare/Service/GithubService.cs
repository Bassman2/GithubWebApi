﻿namespace GithubWebApi.Service;

// https://docs.github.com/en/rest/pulls/pulls?apiVersion=2022-11-28

internal class GithubService : JsonService
{
    private const string defHost = "https://api.github.com";

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

    #region Branches

    public async Task<IEnumerable<BranchModel>?> GetBranchesAsync(string owner, string repo, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<BranchModel>>($"/repos/{owner}/{repo}/branches", cancellationToken);
        return res;
    }

    public async Task<BranchModel?> GetBranchAsync(string owner, string repo, string branch, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<BranchModel>($"/repos/{owner}/{repo}/branches/{branch}", cancellationToken);
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
        var res = await GetFromJsonAsync<UserModel>("/user", cancellationToken);
        return res;
    }

    #endregion
}
