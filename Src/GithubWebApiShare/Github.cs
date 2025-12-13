namespace GithubWebApi;

/// <summary>
/// Provides methods to interact with GitHub's API, including operations for branches, pull requests, repositories, and more.
/// </summary>
public sealed partial class Github: JsonService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Github"/> class using a store key and application name.
    /// </summary>
    /// <param name="storeKey">The key used to store authentication or configuration data.</param>
    /// <param name="appName">The name of the application using the GitHub API.</param>
    public Github(string storeKey, string appName) : base(storeKey, appName, SourceGenerationContext.Default)
    {
        client!.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
        client!.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
        client!.DefaultRequestHeaders.Add("User-Agent", appName);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Github"/> class using a host URI, an optional authenticator, and an application name.
    /// </summary>
    /// <param name="host">The base URI of the GitHub API host.</param>
    /// <param name="authenticator">The authenticator used for API authentication, or <c>null</c> for unauthenticated access.</param>
    /// <param name="appName">The name of the application using the GitHub API.</param>
    public Github(Uri host, IAuthenticator? authenticator, string appName) : base(host, authenticator, appName, SourceGenerationContext.Default)
    {
        client!.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
        client!.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
        client!.DefaultRequestHeaders.Add("User-Agent", appName);
    }

    /// <summary>
    /// Gets the relative URL used to test authentication with the GitHub API.
    /// </summary>
    protected override string? AuthenticationTestUrl => "/";

    //protected override async Task ErrorHandlingAsync(HttpResponseMessage response, string memberName, CancellationToken cancellationToken)
    //{
    //    var error = await ReadFromJsonAsync<ErrorRoot>(response, cancellationToken);
    //    WebServiceException.ThrowHttpError(error?.ToString(), response, memberName);
    //}

    //public async Task<BranchModel?> GetHeadRevisionAsync(string owner, string repo, CancellationToken cancellationToken = default)
    //{
    //    WebServiceException.ThrowIfNotConnected(client);

    //    var res = await service.GetHeadRevisionAsync(owner, repo, cancellationToken);
    //    return res; //?.Select(p => new PullRequest(p)).ToList();
    //}

    /// <summary>
    /// Retrieves the version string of the GitHub API for the authenticated user or application.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A string containing the version information, or <c>null</c> if not available.</returns>
    public override async Task<string?> GetVersionStringAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<MetaModel>($"/meta", cancellationToken);
        return res?.InstalledVersion ?? "Error";
    }


    #region Branches

    /// <summary>
    /// Retrieves all branches for a specified repository.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An asynchronous stream of <see cref="Branch"/> objects.</returns>
    public async IAsyncEnumerable<Branch> GetBranchesAsync(string owner, string repo, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = GetFromJsonYieldAsync<BranchModel>($"/repos/{owner}/{repo}/branches", cancellationToken);
        await foreach (var item in res)
        {
            yield return new Branch(item);
        }
    }

    /// <summary>
    /// Retrieves a specific branch by name.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="branch">The name of the branch.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The <see cref="Branch"/> object if found; otherwise, <c>null</c>.</returns>
    public async Task<Branch?> GetBranchAsync(string owner, string repo, string branch, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<BranchModel>($"/repos/{owner}/{repo}/branches/{branch}", cancellationToken);
        return res is not null ? new Branch(res) : null;
    }

    /// <summary>
    /// Renames an existing branch.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="branch">The current name of the branch.</param>
    /// <param name="newName">The new name for the branch.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The updated <see cref="Branch"/> object if successful; otherwise, <c>null</c>.</returns>
    public async Task<Branch?> RenameBranchAsync(string owner, string repo, string branch, string newName, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = new BranchRenameModel() { NewName = newName };
        var res = await PostAsJsonAsync<BranchRenameModel, BranchModel>($"/repos/{owner}/{repo}/branches/{branch}/rename", req, cancellationToken);
        return res is not null ? new Branch(res) : null;
    }

    /// <summary>
    /// Creates a new branch from the default branch.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="newBranchName">The name of the new branch.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The created <see cref="Reference"/> object if successful; otherwise, <c>null</c>.</returns>
    public async Task<Reference?> CreateBranchAsync(string owner, string repo, string newBranchName, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        // GetHeadReferencesAsync
        var refs = await GetFromJsonAsync<List<ReferenceModel>?>($"/repos/{owner}/{repo}/git/refs/heads", cancellationToken);

        string sha = refs!.Last().Object!.Sha!;

        // var res = await service.CreateHeadReferenceAsync(owner, repo, sha, newBranchName, cancellationToken);
        var req = new RefModel()
        {
            Ref = $"refs/heads/{newBranchName}",
            Sha = sha
        };
        var res = await PostAsJsonAsync<RefModel, ReferenceModel>($"/repos/{owner}/{repo}/git/refs", req, cancellationToken);


        return res.CastModel<Reference>();
    }

    /// <summary>
    /// Creates a new branch from a specified branch.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="branchName">The name of the source branch.</param>
    /// <param name="newBranchName">The name of the new branch.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The created <see cref="Reference"/> object if successful; otherwise, <c>null</c>.</returns>
    public async Task<Reference?> CreateBranchAsync(string owner, string repo, string branchName, string newBranchName, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        // GetHeadReferenceAsync
        var branch = await GetFromJsonAsync<ReferenceModel>($"/repos/{owner}/{repo}/git/ref/heads/{branchName}", cancellationToken);

        //var refs = await service.GetHeadReferencesAsync(owner, repo, cancellationToken);
        //string? sha = refs?.First(r => r.Ref?.Substring(r.Ref.LastIndexOf('/') + 1) == branch)?.Object!.Sha;
        //if (sha == null) throw new ArgumentException(branch, nameof(branch));

        //var res = await service.CreateHeadReferenceAsync(owner, repo, branch.Object!.Sha!, newBranchName, cancellationToken);

        var req = new RefModel()
        {
            Ref = $"refs/heads/{newBranchName}",
            Sha = branch!.Object!.Sha!
        };
        var res = await PostAsJsonAsync<RefModel, ReferenceModel>($"/repos/{owner}/{repo}/git/refs", req, cancellationToken);

        return res.CastModel<Reference>();
    }

    /// <summary>
    /// Deletes a branch from the repository.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="branchName">The name of the branch to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task DeleteBranchAsync(string owner, string repo, string branchName, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var branch = await GetFromJsonAsync<ReferenceModel>($"/repos/{owner}/{repo}/git/ref/heads/{branchName}", cancellationToken);
        //await service.DeleteReferenceAsync(owner, repo, branch.Ref!, cancellationToken);

        await DeleteAsync($"/repos/{owner}/{repo}/git/{branch!.Ref!}", cancellationToken);

    }

    /// <summary>
    /// Retrieves a list of codespaces for the specified repository.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A list of <see cref="Branch"/> objects representing the codespaces, or <c>null</c> if none are found.</returns>
    public async Task<List<Branch>?> GetCodespacesAsync(string owner, string repo, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<List<BranchModel>>($"/repos/{owner}/{repo}/codespaces", cancellationToken);
        return res.CastModel<Branch>();
    }


    #endregion

    #region Pull Request

    /// <summary>
    /// Retrieves all pull requests for a specified repository.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A collection of <see cref="PullRequest"/> objects if found; otherwise, <c>null</c>.</returns>
    public async Task<IEnumerable<PullRequest>?> GetPullsAsync(string owner, string repo, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<List<PullModel>>($"/repos/{owner}/{repo}/pulls", cancellationToken);
        return res?.Select(p => new PullRequest(p)).ToList();
    }

    /// <summary>
    /// Creates a new pull request in the specified repository.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="title">The title of the pull request.</param>
    /// <param name="body">The body or description of the pull request.</param>
    /// <param name="head">The name of the branch where changes are implemented.</param>
    /// <param name="baseBranch">The name of the branch you want to merge into.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The created <see cref="PullRequest"/> object if successful; otherwise, <c>null</c>.</returns>
    public async Task<PullRequest?> CreatePullAsync(string owner, string repo, string title, string body, string head, string baseBranch, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = new PullCreateModel() { Title = title, Body = body, Head = head, Base = baseBranch };
        var res = await PostAsJsonAsync<PullCreateModel, PullModel>($"/repos/{owner}/{repo}/pulls", req, cancellationToken);
        return res is not null ? new PullRequest(res) : null;
    }

    /// <summary>
    /// Retrieves a specific pull request by its number.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="pullNumber">The number of the pull request.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The <see cref="PullRequest"/> object if found; otherwise, <c>null</c>.</returns>
    public async Task<PullRequest?> GetPullAsync(string owner, string repo, int pullNumber, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<PullModel>($"/repos/{owner}/{repo}/pulls/{pullNumber}", cancellationToken);
        return res is not null ? new PullRequest(res) : null;
    }

    /// <summary>
    /// Updates an existing pull request in the specified repository.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="pullNumber">The number of the pull request to update.</param>
    /// <param name="title">The updated title of the pull request.</param>
    /// <param name="body">The updated body or description of the pull request.</param>
    /// <param name="state">The updated state of the pull request (e.g., "open" or "closed").</param>
    /// <param name="baseBranch">The updated base branch for the pull request.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The updated <see cref="PullRequest"/> object if successful; otherwise, <c>null</c>.</returns>
    public async Task<PullRequest?> UpdatePullAsync(string owner, string repo, int pullNumber, string title, string body, string state, string baseBranch, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = new PullPatchModel() { Title = title, Body = body, State = state, Base = baseBranch };
        var res = await PatchAsJsonAsync<PullPatchModel, PullModel>($"/repos/{owner}/{repo}/pulls/{pullNumber}", req, cancellationToken);
        return res is not null ? new PullRequest(res) : null;
    }

    //public async Task<PullModel?> UpdatePullBranchAsync(string owner, string repo, int pullNumber, CancellationToken cancellationToken)
    //{
    //    var req = new PullPatchModel() { };
    //    var res = await PatchAsJsonAsync<PullPatchModel, PullModel>($"/repos/{owner}/{repo}/pulls/{pullNumber}", req, cancellationToken);
    //    return res;
    //}

    #endregion

    #region Release

    /// <summary>
    /// Creates a new release in the specified repository.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="tag">The tag associated with the release (e.g., "v1.0.0").</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The created <see cref="Release"/> object if successful; otherwise, <c>null</c>.</returns>
    public async Task<Release?> CreateReleaseAsync(string owner, string repo, object tag, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = new ReleaseCreateModel()
        {
            TagName = "v1.0.0",
            TargetCommitish = "master",
            Name = "v1.0.0",
            Body = "Description of the release",
            Draft = false,
            Prerelease = false,
            GenerateReleaseNotes = false
        }; //
        var res = await PostAsJsonAsync<ReleaseCreateModel, ReleaseModel>($"/repos/{owner}/{repo}/releases", req, cancellationToken);
        return res.CastModel<Release>();
    }

    #endregion
    
    #region Repositories

    /// <summary>
    /// Retrieves all repositories for a specified organization.
    /// </summary>
    /// <param name="org">The name of the organization.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An asynchronous stream of <see cref="Repository"/> objects.</returns>
    public async IAsyncEnumerable<Repository> GetOrganizationRepositoriesAsync(string org, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = GetFromJsonYieldAsync<RepositoryModel>($"/orgs/{org}/repos", cancellationToken);
        if (res is not null)
        {
            await foreach (var item in res)
            {
                yield return item.CastModel<Repository>()!;
            }
        }
    }

    /// <summary>
    /// Retrieves all repositories for the authenticated user.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An asynchronous stream of <see cref="Repository"/> objects.</returns>
    public async IAsyncEnumerable<Repository> GetAuthenticatedUserRepositoriesAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = GetFromJsonYieldAsync<RepositoryModel>($"/user/repos", cancellationToken);
        if (res is not null)
        {
            await foreach (var item in res)
            {
                yield return item.CastModel<Repository>()!;
            }
        }
    }

    /// <summary>
    /// Retrieves all repositories for a specified user.
    /// </summary>
    /// <param name="user">The username of the user.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An asynchronous stream of <see cref="Repository"/> objects.</returns>
    public async IAsyncEnumerable<Repository> GetUserRepositoriesAsync(string user, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = GetFromJsonYieldAsync<RepositoryModel>($"/users/{user}/repos", cancellationToken);
        if (res is not null)
        {
            await foreach (var item in res)
            {
                yield return item.CastModel<Repository>()!;
            }
        }
    }

    /// <summary>
    /// Retrieves all public repositories starting from a specified ID.
    /// </summary>
    /// <param name="since">The ID of the last repository seen.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An asynchronous stream of <see cref="Repository"/> objects.</returns>
    public async IAsyncEnumerable<Repository> GetPublicRepositoriesAsync(int since, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = GetFromJsonYieldAsync<RepositoryModel>($"/repositories?since={since}", cancellationToken);
        if (res is not null)
        {
            await foreach (var item in res)
            {
                yield return item.CastModel<Repository>()!;
            }
        }
    }

    /// <summary>
    /// Retrieves a specific repository by its owner and name.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The <see cref="Repository"/> object if found; otherwise, <c>null</c>.</returns>
    public async Task<Repository?> GetRepositoryAsync(string owner, string repo, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<RepositoryModel>($"/repos/{owner}/{repo}", cancellationToken);
        return res.CastModel<Repository>();
    }

    //public async Task<List<TagModel>?> GetRepositoryTagsAsync(string org, string repo, CancellationToken cancellationToken)
    //{
    //    var res = await GetFromJsonAsync<List<TagModel>>($"/repos/{org}/{repo}/tags", cancellationToken);
    //    return res;
    //}

    //public async Task<List<TeamModel>?> GetRepositoryTeamsAsync(string org, string repo, CancellationToken cancellationToken)
    //{
    //    var res = await GetFromJsonAsync<List<TeamModel>>($"/repos/{org}/{repo}/teams", cancellationToken);
    //    return res;
    //}

    #endregion

    #region Repository Contents

    /// <summary>
    /// Retrieves the content of a file or directory in a repository.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="path">The path to the file or directory.</param>
    /// <param name="reference">The name of the commit/branch/tag. Defaults to the repository's default branch if not specified.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The <see cref="Content"/> object representing the file or directory content, or <c>null</c> if not found.</returns>
    public async Task<Content?> GetRepositoryContentAsync(string owner, string repo, string path, string? reference, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl($"/repos/{owner}/{repo}/contents/{path}", ("ref", reference));
        var res = await GetFromJsonAsync<ContentModel>(req, cancellationToken);
        return res.CastModel<Content>();
    }

    /// <summary>
    /// Retrieves the content of a file in a repository as a string.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="path">The path to the file.</param>
    /// <param name="reference">The name of the commit/branch/tag. Defaults to the repository's default branch if not specified.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The content of the file as a string, or <c>null</c> if not found.</returns>
    public async Task<string?> GetRepositoryContentStringAsync(string owner, string repo, string path, string? reference, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var req = CombineUrl($"/repos/{owner}/{repo}/contents/{path}", ("ref", reference));
        var res = await GetFromJsonAsync<ContentModel>(req, cancellationToken);
        return (res?.Type == "") ? Encoding.UTF8.GetString(Convert.FromBase64String(res?.Content!)) : await GetStringAsync(res!.DownloadUrl!, cancellationToken);
    }

    /// <summary>
    /// Creates or updates the content of a file in a repository.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="path">The path to the file.</param>
    /// <param name="message">The commit message.</param>
    /// <param name="content">The new content of the file, encoded in Base64.</param>
    /// <param name="sha">The blob SHA of the file being replaced. Required if updating an existing file.</param>
    /// <param name="branch">The branch name where the changes will be made. Defaults to the repository's default branch if not specified.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The <see cref="ContentCommit"/> object representing the commit details, or <c>null</c> if the operation fails.</returns>
    public async Task<ContentCommit?> CreateOrUpdateFileContentsAsync(string owner, string repo, string path, string message, string content, string? sha, string? branch, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        ContentCreateModel reqModel = new()
        {
            Message = message,
            Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(content)),
            Sha = sha,
            Branch = branch,
            Committer = null,
            Author = null,
        };
        var res = await PutAsJsonAsync<ContentCreateModel, ContentCommitModel>($"/repos/{owner}/{repo}/contents/{path}", reqModel, cancellationToken);
        return res.CastModel<ContentCommit>();
    }

    /// <summary>
    /// Deletes a file from a repository.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="path">The path to the file to delete.</param>
    /// <param name="message">The commit message.</param>
    /// <param name="sha">The blob SHA of the file to delete.</param>
    /// <param name="branch">The branch name where the changes will be made. Defaults to the repository's default branch if not specified.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The <see cref="ContentCommit"/> object representing the commit details, or <c>null</c> if the operation fails.</returns>
    public async Task<ContentCommit?> DeleteFileAsync(string owner, string repo, string path, string message, string? sha, string? branch, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        ContentCreateModel reqModel = new()
        {
            Message = message,
            Sha = sha,
            Branch = branch,
            Committer = null,
            Author = null
        };

        var res = await DeleteJsonAsync<ContentCreateModel, ContentCommitModel>($"/repos/{owner}/{repo}/contents/{path}", reqModel, cancellationToken);
        return res.CastModel<ContentCommit>();
    }

    #endregion

    #region Tags

    /// <summary>
    /// Retrieves all tags for a specified repository.
    /// </summary>
    /// <param name="org">The organisation of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A collection of <see cref="Tag"/> objects if found; otherwise, <c>null</c>.</returns>
    public async Task<IEnumerable<Tag>?> GetRepositoryTagsAsync(string org, string repo, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<List<TagModel>>($"/repos/{org}/{repo}/tags", cancellationToken);
        return res.CastModel<Tag>();
    }

    /// <summary>
    /// Retrieves a specific tag by its name in a repository.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="tag">The name of the tag.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The <see cref="Tag"/> object if found; otherwise, <c>null</c>.</returns>
    public async Task<Tag?> GetTagAsync(string owner, string repo, string tag, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<TagModel>($"/repos/{owner}/{repo}/git/tags", cancellationToken);
        return res.CastModel<Tag>();
    }

    /// <summary>
    /// Retrieves all tag references for a specified repository.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A collection of <see cref="Reference"/> objects representing tag references if found; otherwise, <c>null</c>.</returns>
    public async Task<IEnumerable<Reference>?> GetTagsAsync(string owner, string repo, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<List<ReferenceModel>?>($"/repos/{owner}/{repo}/git/refs/tags", cancellationToken);
        return res.CastModel<Reference>();
    }

    //public async Task<Reference?> GetTagAsync(string owner, string repo, string tagName, CancellationToken cancellationToken = default)
    //{
    //    WebServiceException.ThrowIfNotConnected(client);

    //    var res = await service.GetTagAsync(owner, repo, tagName, cancellationToken);
    //    return res.CastModel<Reference>();
    //}

    //public async Task<Tag> CreateTagAsync(string owner, string repo, string tagName, CancellationToken cancellationToken = default)
    //{
    //    WebServiceException.ThrowIfNotConnected(client);

    //    var res = service.GetTagReferenceAsync(owner, repo, tagName, cancellationToken);
    //    return res.CastModel<Tag>();
    //}

    //public async Task DeleteTagAsync(string owner, string repo, string tagName, CancellationToken cancellationToken = default)
    //{
    //    WebServiceException.ThrowIfNotConnected(client);

    //    service.DeleteReferenceAsync(owner, repo, tag.Ref, cancellationToken);
    //}

    #endregion

    #region Trees

    /// <summary>
    /// Retrieves a Git tree by its SHA hash.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="treeSha">The SHA hash of the tree to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The <see cref="Tree"/> object if found; otherwise, <c>null</c>.</returns>
    public async Task<Tree?> GetTreeAsync(string owner, string repo, string treeSha, CancellationToken cancellationToken = default)
         => await GetTreeAsync(owner, repo, treeSha, false, cancellationToken);

    /// <summary>
    /// Retrieves a Git tree by its SHA hash, with an option to include subtrees recursively.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="treeSha">The SHA hash of the tree to retrieve.</param>
    /// <param name="recursive">A value indicating whether to include subtrees recursively.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The <see cref="Tree"/> object if found; otherwise, <c>null</c>.</returns>
    public async Task<Tree?> GetTreeAsync(string owner, string repo, string treeSha, bool recursive, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        string req = CombineUrl($"/repos/{owner}/{repo}/git/trees/{treeSha}", ("recursive", recursive ? "1" : null));
        var res = await GetFromJsonAsync<TreeModel>(req, cancellationToken);
        return res.CastModel<Tree>();
    }

    //public async Task<Tree?> GetTreePathAsync(string owner, string repo, string treeSha, string path, CancellationToken cancellationToken = default)
    //{
    //    WebServiceException.ThrowIfNotConnected(client);

    //    var res = await service.GetTreeAsync(owner, repo, treeSha, false, cancellationToken);

    //    var pathItems = path.Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);
    //    foreach (var pathItem in pathItems)
    //    {
    //        var sha = res?.Trees?.FirstOrDefault(t => string.Equals(t.Path, pathItem, StringComparison.OrdinalIgnoreCase))?.Sha;
    //        if (sha == null) return null;

    //        res = await service.GetTreeAsync(owner, repo, sha, false, cancellationToken);
    //    }
    //    return res.CastModel<Tree>();
    //}

    /// <summary>
    /// Retrieves a Git tree for a specific path within a repository.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="treeSha">The SHA hash of the tree to retrieve.</param>
    /// <param name="path">The path within the tree to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The <see cref="Tree"/> object representing the specified path if found; otherwise, <c>null</c>.</returns>
    public async Task<Tree?> GetTreePathAsync(string owner, string repo, string treeSha, string path, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        string req = CombineUrl($"/repos/{owner}/{repo}/git/trees/{treeSha}");
        var res = await GetFromJsonAsync<TreeModel>(req, cancellationToken);
        if (res == null) return null;
        if (res.Truncated)
        {
            // tree has more elements as GetTreeAsync can get, so step into path
            var pathItems = path.Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);
            foreach (var pathItem in pathItems)
            {
                var sha = res?.Trees?.FirstOrDefault(t => string.Equals(t.Path, pathItem, StringComparison.OrdinalIgnoreCase))?.Sha;
                if (sha == null) return null;

                string reqx = CombineUrl($"/repos/{owner}/{repo}/git/trees/{treeSha}");
                var resx = await GetFromJsonAsync<TreeModel>(reqx, cancellationToken);
            }
            return res.CastModel<Tree>();
        }
        else
        {
            var list = res?.Trees?.ToList();
            path = path.Replace('\\', '/').Trim('/') + '/';
            int pathLength = path.Length;
            var items = res?.Trees?.Where(i => i.Path!.StartsWith(path)).ToList();
            items?.ForEach(i => i.Path = i.Path?[pathLength..]);
            res!.Trees = items;
            return res.CastModel<Tree>();
        }
    }

    #endregion

    #region User

    /// <summary>
    /// Retrieves the authenticated user's details.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The <see cref="User"/> object representing the authenticated user, or <c>null</c> if not found.</returns>
    public async Task<User?> GetAuthenticatedUserAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<UserModel>("/user", cancellationToken);
        return res.CastModel<User>(); 
    }

    /// <summary>
    /// Retrieves a user's details by their username.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The <see cref="User"/> object representing the user, or <c>null</c> if not found.</returns>
    public async Task<User?> GetUserAsync(string username, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<UserModel>($"/users/{username}", cancellationToken);
        return res.CastModel<User>();
    }

    /// <summary>
    /// Retrieves a user's details by their unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the user.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The <see cref="User"/> object representing the user, or <c>null</c> if not found.</returns>
    public async Task<User?> GetUserAsync(long id, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<UserModel>($"/user/{id}", cancellationToken);
        return res.CastModel<User>();
    }

    #endregion

    #region Workflows

    public async Task<IEnumerable<Workflow>?> GetRepositoryWorkflowsAsync(string owner, string repo, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<WorkflowListModel>($"/repos/{owner}/{repo}/actions/workflows", cancellationToken);
        return res?.Workflows.CastModel<Workflow>();
    }

    #endregion

    #region Workflows Runs

    public async Task<IEnumerable<WorkflowRun>?> GetRepositoryWorkflowRunsAsync(string owner, string repo, string? branch, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var url = CombineUrl($"/repos/{owner}/{repo}/actions/runs", ("branch", branch));
        var res = await GetFromJsonAsync<WorkflowRunListModel>(url, cancellationToken);
        return res?.WorkflowRuns.CastModel<WorkflowRun>();
    }

    public async Task<IEnumerable<WorkflowRun>?> GetWorkflowsRunsAsync(string owner, string repo, string workflowId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<WorkflowRunListModel>($"/repos/{owner}/{repo}/actions/workflows/{workflowId}/runs", cancellationToken);
        return res?.WorkflowRuns.CastModel<WorkflowRun>();
    }          

    #endregion


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

            //var res = await ReadFromJsonAsync<List<T>?>(response, cancellationToken);

            JsonTypeInfo<List<T>?> jsonTypeInfoOut = (JsonTypeInfo<List<T>?>)context.GetTypeInfo(typeof(List<T>))!;
            var res = await response.Content.ReadFromJsonAsync<List<T>?>(jsonTypeInfoOut, cancellationToken);


            if (res != null)
            {
                foreach (var item in res)
                {
                    yield return item;
                }
            }
            requestUri = NextLink(response);
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

    /// <summary>
    /// Retrieves GitHub API metadata for the authenticated user or application.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A string containing the metadata information, or <c>null</c> if not available.</returns>
    public async Task<Meta?> GetMetaAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<MetaModel>($"/meta", cancellationToken);
        return res.CastModel<Meta>();
    }

    /// <summary>
    /// Retrieves the head revision (latest commit) branch for the specified repository.
    /// </summary>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="repo">The name of the repository.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The <see cref="Branch"/> object representing the head revision, or <c>null</c> if not found.</returns>
    public async Task<Branch?> GetHeadRevisionAsync(string owner, string repo, CancellationToken cancellationToken = default)
    {
        var res = await GetFromJsonAsync<BranchModel>($"/repos/{owner}/{repo}/git/refs/heads", cancellationToken);
        return res.CastModel<Branch>();
    }
}
