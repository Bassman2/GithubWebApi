using GithubWebApi.Service.Model;
using Microsoft.VisualBasic.FileIO;
using System.Threading;

namespace GithubWebApi.Service;

// https://docs.github.com/en/rest/pulls/pulls?apiVersion=2022-11-28

// https://stackoverflow.com/questions/25022016/get-all-file-names-from-a-github-repo-through-the-github-api

internal partial class GithubService : JsonService
{
    private const string defHost = "https://api.github.com";

    protected override string? AuthenticationTestUrl => "/user";

    public GithubService(Uri host, IAuthenticator? authenticator, string appName)
    : base(host, authenticator, appName, SourceGenerationContext.Default)
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

    /// <summary>
    /// Create a new branch from the main branch
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="repo"></param>
    /// <param name="newBranchName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

    // https://docs.github.com/de/rest/git/refs?apiVersion=2022-11-28#create-a-reference
    public async Task<ReferenceModel?> CreateBranchAsync(string owner, string repo, string newBranchName, CancellationToken cancellationToken)
    {
        var refs = await GetFromJsonAsync<IEnumerable<ReferenceModel>?>($"/repos/{owner}/{repo}/git/refs/heads", cancellationToken);

        var req = new RefModel()
        {
            Ref = $"refs/heads/{newBranchName}",
            Sha = refs!.First().Object!.Sha
        };
        var res = await PostAsJsonAsync<RefModel, ReferenceModel>($"/repos/{owner}/{repo}/git/refs", req, cancellationToken);
        return res;
    }

    /// <summary>
    /// Create a new branch from the given branch 
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="repo"></param>
    /// <param name="branch"></param>
    /// <param name="newBranchName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    // https://docs.github.com/de/rest/git/refs?apiVersion=2022-11-28#create-a-reference
    public async Task<ReferenceModel?> CreateBranchAsync(string owner, string repo, string branch, string newBranchName, CancellationToken cancellationToken)
    {
        RefModel? _ref = await GetFromJsonAsync<RefModel>($"/repos/{owner}/{repo}/git/refs/heads/{branch}", cancellationToken);

        var req = new RefModel() 
        { 
            Ref = $"refs/heads/{newBranchName}", 
            Sha = _ref!.Sha 
        };
        var res = await PostAsJsonAsync<RefModel, ReferenceModel>($"/repos/{owner}/{repo}/git/refs", req, cancellationToken);
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

    #region References

    // https://docs.github.com/en/rest/git/refs?apiVersion=2022-11-28

    
    public async Task<ReferenceModel?> GetHeadReferenceAsync(string owner, string repo, string branchName, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<ReferenceModel>($"/repos/{owner}/{repo}/git/ref/heads/{branchName}", cancellationToken);
        return res;
    }

    public async Task<ReferenceModel?> GetTagReferenceAsync(string owner, string repo, string tagName, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<ReferenceModel>($"/repos/{owner}/{repo}/git/ref/tags/{tagName}", cancellationToken);
        return res;
    }

    public async Task<IEnumerable<ReferenceModel>?> GetReferencesAsync(string owner, string repo, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<ReferenceModel>?>($"/repos/{owner}/{repo}/git/refs", cancellationToken);
        return res;
    }

    public async Task<IEnumerable<ReferenceModel>?> GetHeadReferencesAsync(string owner, string repo, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<ReferenceModel>?>($"/repos/{owner}/{repo}/git/refs/heads", cancellationToken);
        return res;
    }

    public async Task<IEnumerable<ReferenceModel>?> GetTagReferencesAsync(string owner, string repo, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<ReferenceModel>?>($"/repos/{owner}/{repo}/git/refs/tags", cancellationToken);
        return res;
    }

    public async Task<ReferenceModel?> CreateHeadReferenceAsync(string owner, string repo, string reference, string name, CancellationToken cancellationToken)
    {
        var req = new RefModel()
        {
            Ref = $"refs/heads/{name}",
            Sha = reference
        };
        var res = await PostAsJsonAsync<RefModel, ReferenceModel>($"/repos/{owner}/{repo}/git/refs", req, cancellationToken);
        return res;
    }

    public async Task<ReferenceModel?> CreateTagReferenceAsync(string owner, string repo, string reference, string name, CancellationToken cancellationToken)
    {
        var req = new RefModel()
        {
            Ref = $"refs/tags/{name}",
            Sha = reference
        };
        var res = await PostAsJsonAsync<RefModel, ReferenceModel>($"/repos/{owner}/{repo}/git/refs", req, cancellationToken);
        return res;
    }

    public async Task DeleteReferenceAsync(string owner, string repo, string reference, CancellationToken cancellationToken)
    {
        await DeleteAsync($"/repos/{owner}/{repo}/git/{reference}", cancellationToken);
    }

    #endregion

    #region Releases

    // https://docs.github.com/en/rest/releases/releases?apiVersion=2022-11-28

    public async Task<ReleaseModel?> CreateRelease(string owner, string repo, object tag, CancellationToken cancellationToken)
    {
        var req = new ReleaseCreateModel()
        {
            TagName = "v1.0.0",
             TargetCommitish = "master",
             Name = "v1.0.0" ,
             Body = "Description of the release",
             Draft = false,
             Prerelease = false,
             GenerateReleaseNotes = false
        };
        var res = await PostAsJsonAsync<ReleaseCreateModel, ReleaseModel>($"/repos/{owner}/{repo}/releases", req, cancellationToken);
        return res;
    }


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

    #region Repository Contents

    public async Task<ContentModel?> GetRepositoryContentAsync(string owner, string repo, string path, string? reference, CancellationToken cancellationToken)
    {
        var req = CombineUrl($"/repos/{owner}/{repo}/contents/{path}", ("ref", reference));
        var res = await GetFromJsonAsync<ContentModel>(req, cancellationToken);
        return res;
    }

    public async Task<ContentCommitModel?> CreateOrUpdateFileContentsAsync(string owner, string repo, string path, string message, string content, string? sha, string? branch, UserModel? committer, UserModel? author, CancellationToken cancellationToken)
    {
        ContentCreateModel reqModel = new()
        {
            Message = message,
            Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(content)),
            Sha = sha,
            Branch = branch,
            Committer = committer,
            Author = author,
        };
        
        var res = await PutAsJsonAsync<ContentCreateModel, ContentCommitModel>($"/repos/{owner}/{repo}/contents/{path}", reqModel, cancellationToken);
        return res;
    }

    public async Task<ContentCommitModel?> DeleteFileAsync(string owner, string repo, string path, string message, string? sha, string? branch, UserModel? committer, UserModel? author, CancellationToken cancellationToken)
    {
        ContentCreateModel reqModel = new()
        {
            Message = message,
            Sha = sha,
            Branch = branch,
            Committer = committer,
            Author = author,
        };

        var res = await DeleteJsonAsync<ContentCreateModel, ContentCommitModel>($"/repos/{owner}/{repo}/contents/{path}", reqModel, cancellationToken);
        return res;
    }

    #endregion

    #region Tags /repos/{owner}/{repo}/git/tags

    //public async Task<TagModel?> GetTagAsync(string owner, string repo, string tagSha, CancellationToken cancellationToken)
    //{
    //    var res = await GetFromJsonAsync<TagModel>($"/repos/{owner}/{repo}/git/tags/{tagSha}", cancellationToken);
    //    return res;
    //}

    public async Task<TagModel?> GetTagAsync(string owner, string repo, string tagSha, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<TagModel>($"/repos/{owner}/{repo}/git/tags", cancellationToken);
        return res;
    }

    #endregion

    #region Tree

    // https://docs.github.com/en/rest/git/trees?apiVersion=2022-11-28

    public async Task<TreeModel?> CreateTreeAsync(string owner, string repo, TreeCreateModel req, CancellationToken cancellationToken)
    {
        var res = await PostAsJsonAsync<TreeCreateModel, TreeModel>($"/repos/{owner}/{repo}/git/trees", req, cancellationToken);
        return res;
    }

    public async Task<TreeModel?> GetTreeAsync(string owner, string repo, string treeSha, bool recursive, CancellationToken cancellationToken)
    {
        string req = CombineUrl($"/repos/{owner}/{repo}/git/trees/{treeSha}", ("recursive", recursive ? "1" : null));
        var res = await GetFromJsonAsync<TreeModel>(req, cancellationToken);
        return res;
    }

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
