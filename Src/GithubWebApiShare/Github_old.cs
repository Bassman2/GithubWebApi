//namespace GithubWebApi;

///// <summary>
///// Provides methods to interact with GitHub's API, including operations for branches, pull requests, repositories, and more.
///// </summary>
//public sealed class Github_old : IDisposable
//{
//    private GithubService? service;

//    /// <summary>
//    /// Initializes a new instance of the <see cref="Github"/> class using a store key and application name.
//    /// </summary>
//    /// <param name="storeKey">The key to retrieve GitHub credentials from the key store.</param>
//    /// <param name="appName">The name of the application.</param>
//    public Github(string storeKey, string appName)
//        : this(new Uri(KeyStore.Key(storeKey)?.Host!), KeyStore.Key(storeKey)!.Token!, appName)
//    { }

//    /// <summary>
//    /// Initializes a new instance of the <see cref="Github"/> class using a host URI, token, and application name.
//    /// </summary>
//    /// <param name="host">The GitHub API host URI.</param>
//    /// <param name="token">The authentication token.</param>
//    /// <param name="appName">The name of the application.</param>
//    public Github(Uri host, string token, string appName)
//    {
//        service = new GithubService(host, new BearerAuthenticator(token), appName);
//    }

//    /// <summary>
//    /// Releases the resources used by the <see cref="Github"/> instance.
//    /// </summary>
//    public void Dispose()
//    {
//        if (this.service != null)
//        {
//            this.service.Dispose();
//            this.service = null;
//        }
//        GC.SuppressFinalize(this);
//    }

//    //public async Task<BranchModel?> GetHeadRevisionAsync(string owner, string repo, CancellationToken cancellationToken = default)
//    //{
//    //    WebServiceException.ThrowIfNullOrNotConnected(this.service);

//    //    var res = await service.GetHeadRevisionAsync(owner, repo, cancellationToken);
//    //    return res; //?.Select(p => new PullRequest(p)).ToList();
//    //}

//    #region Branches

//    /// <summary>
//    /// Retrieves all branches for a specified repository.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>An asynchronous stream of <see cref="Branch"/> objects.</returns>
//    public async IAsyncEnumerable<Branch> GetBranchesAsync(string owner, string repo, [EnumeratorCancellation] CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = service.GetBranchesAsync(owner, repo, cancellationToken);
//        await foreach (var item in res)
//        {
//            yield return new Branch(item);
//        }
//    }

//    /// <summary>
//    /// Retrieves a specific branch by name.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="branch">The name of the branch.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The <see cref="Branch"/> object if found; otherwise, <c>null</c>.</returns>
//    public async Task<Branch?> GetBranchAsync(string owner, string repo, string branch, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetBranchAsync(owner, repo, branch, cancellationToken);
//        return res is not null ? new Branch(res) : null;
//    }

//    /// <summary>
//    /// Renames an existing branch.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="branch">The current name of the branch.</param>
//    /// <param name="newName">The new name for the branch.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The updated <see cref="Branch"/> object if successful; otherwise, <c>null</c>.</returns>
//    public async Task<Branch?> RenameBranchAsync(string owner, string repo, string branch, string newName, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);
                
//        var res = await service.RenameBranchAsync(owner, repo, branch, newName, cancellationToken);
//        return res is not null ? new Branch(res) : null;
//    }

//    /// <summary>
//    /// Creates a new branch from the default branch.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="newBranchName">The name of the new branch.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The created <see cref="Reference"/> object if successful; otherwise, <c>null</c>.</returns>
//    public async Task<Reference?> CreateBranchAsync(string owner, string repo, string newBranchName, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var refs = await service.GetHeadReferencesAsync(owner, repo, cancellationToken);
//        string sha = refs!.Last().Object!.Sha!;

//        var res = await service.CreateHeadReferenceAsync(owner, repo, sha, newBranchName, cancellationToken);
//        return res.CastModel<Reference>();
//    }

//    /// <summary>
//    /// Creates a new branch from a specified branch.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="branchName">The name of the source branch.</param>
//    /// <param name="newBranchName">The name of the new branch.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The created <see cref="Reference"/> object if successful; otherwise, <c>null</c>.</returns>
//    public async Task<Reference?> CreateBranchAsync(string owner, string repo, string branchName, string newBranchName, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var branch = await service.GetHeadReferenceAsync(owner, repo, branchName, cancellationToken) ?? throw new ArgumentException("Branch not found", nameof(branchName));

//        //var refs = await service.GetHeadReferencesAsync(owner, repo, cancellationToken);
//        //string? sha = refs?.First(r => r.Ref?.Substring(r.Ref.LastIndexOf('/') + 1) == branch)?.Object!.Sha;
//        //if (sha == null) throw new ArgumentException(branch, nameof(branch));

//        var res = await service.CreateHeadReferenceAsync(owner, repo, branch.Object!.Sha!, newBranchName, cancellationToken);
//        return res.CastModel<Reference>();
//    }

//    /// <summary>
//    /// Deletes a branch from the repository.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="branchName">The name of the branch to delete.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>A task that represents the asynchronous operation.</returns>
//    public async Task DeleteBranchAsync(string owner, string repo, string branchName, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var branch = await service.GetHeadReferenceAsync(owner, repo, branchName, cancellationToken) ?? throw new ArgumentException("Branch not found", nameof(branchName));
//        await service.DeleteReferenceAsync(owner, repo, branch.Ref!, cancellationToken);
//    }

//    #endregion

//    #region Pull Request

//    /// <summary>
//    /// Retrieves all pull requests for a specified repository.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>A collection of <see cref="PullRequest"/> objects if found; otherwise, <c>null</c>.</returns>
//    public async Task<IEnumerable<PullRequest>?> GetPullsAsync(string owner, string repo, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetPullsAsync(owner, repo, cancellationToken);
//        return res?.Select(p => new PullRequest(p)).ToList();
//    }

//    /// <summary>
//    /// Creates a new pull request in the specified repository.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="title">The title of the pull request.</param>
//    /// <param name="body">The body or description of the pull request.</param>
//    /// <param name="head">The name of the branch where changes are implemented.</param>
//    /// <param name="baseBranch">The name of the branch you want to merge into.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The created <see cref="PullRequest"/> object if successful; otherwise, <c>null</c>.</returns>
//    public async Task<PullRequest?> CreatePullAsync(string owner, string repo, string title, string body, string head, string baseBranch, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.CreatePullAsync(owner, repo, title, body, head, baseBranch, cancellationToken);
//        return res is not null ? new PullRequest(res) : null;
//    }

//    /// <summary>
//    /// Retrieves a specific pull request by its number.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="pullNumber">The number of the pull request.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The <see cref="PullRequest"/> object if found; otherwise, <c>null</c>.</returns>
//    public async Task<PullRequest?> GetPullAsync(string owner, string repo, int pullNumber, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetPullAsync(owner, repo, pullNumber, cancellationToken);
//        return res is not null ? new PullRequest(res) : null;
//    }

//    /// <summary>
//    /// Updates an existing pull request in the specified repository.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="pullNumber">The number of the pull request to update.</param>
//    /// <param name="title">The updated title of the pull request.</param>
//    /// <param name="body">The updated body or description of the pull request.</param>
//    /// <param name="state">The updated state of the pull request (e.g., "open" or "closed").</param>
//    /// <param name="baseBranch">The updated base branch for the pull request.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The updated <see cref="PullRequest"/> object if successful; otherwise, <c>null</c>.</returns>
//    public async Task<PullRequest?> UpdatePullAsync(string owner, string repo, int pullNumber, string title, string body, string state, string baseBranch, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.UpdatePullAsync(owner, repo, pullNumber, title, body, state, baseBranch, cancellationToken);
//        return res is not null ? new PullRequest(res) : null;
//    }

//    #endregion

//    #region Release

//    /// <summary>
//    /// Creates a new release in the specified repository.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="tag">The tag associated with the release (e.g., "v1.0.0").</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The created <see cref="Release"/> object if successful; otherwise, <c>null</c>.</returns>
//    public async Task<Release?> CreateReleaseAsync(string owner, string repo, object tag, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.CreateReleaseAsync(owner, repo, tag, cancellationToken);
//        return res.CastModel<Release>();
//    }

//    #endregion
    
//    #region Repositories

//    /// <summary>
//    /// Retrieves all repositories for a specified organization.
//    /// </summary>
//    /// <param name="org">The name of the organization.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>An asynchronous stream of <see cref="Repository"/> objects.</returns>
//    public async IAsyncEnumerable<Repository> GetOrganizationRepositoriesAsync(string org, [EnumeratorCancellation] CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = service.GetOrganizationRepositoriesAsync(org, cancellationToken);
//        if (res is not null)
//        {
//            await foreach (var item in res)
//            {
//                yield return item.CastModel<Repository>()!;
//            }
//        }
//    }

//    /// <summary>
//    /// Retrieves all repositories for the authenticated user.
//    /// </summary>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>An asynchronous stream of <see cref="Repository"/> objects.</returns>
//    public async IAsyncEnumerable<Repository> GetAuthenticatedUserRepositoriesAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = service.GetAuthenticatedUserRepositoriesAsync(cancellationToken);
//        if (res is not null)
//        {
//            await foreach (var item in res)
//            {
//                yield return item.CastModel<Repository>()!;
//            }
//        }
//    }

//    /// <summary>
//    /// Retrieves all repositories for a specified user.
//    /// </summary>
//    /// <param name="user">The username of the user.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>An asynchronous stream of <see cref="Repository"/> objects.</returns>
//    public async IAsyncEnumerable<Repository> GetUserRepositoriesAsync(string user, [EnumeratorCancellation] CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = service.GetUserRepositoriesAsync(user, cancellationToken);
//        if (res is not null)
//        {
//            await foreach (var item in res)
//            {
//                yield return item.CastModel<Repository>()!;
//            }
//        }
//    }

//    /// <summary>
//    /// Retrieves all public repositories starting from a specified ID.
//    /// </summary>
//    /// <param name="since">The ID of the last repository seen.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>An asynchronous stream of <see cref="Repository"/> objects.</returns>
//    public async IAsyncEnumerable<Repository> GetPublicRepositoriesAsync(int since, [EnumeratorCancellation] CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = service.GetPublicRepositoriesAsync(since, cancellationToken);
//        if (res is not null)
//        {
//            await foreach (var item in res)
//            {
//                yield return item.CastModel<Repository>()!;
//            }
//        }
//    }

//    /// <summary>
//    /// Retrieves a specific repository by its owner and name.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The <see cref="Repository"/> object if found; otherwise, <c>null</c>.</returns>
//    public async Task<Repository?> GetRepositoryAsync(string owner, string repo, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetRepositoryAsync(owner, repo, cancellationToken);
//        return res.CastModel<Repository>();
//    }

//    #endregion
    
//    #region Repository Contents

//    /// <summary>
//    /// Retrieves the content of a file or directory in a repository.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="path">The path to the file or directory.</param>
//    /// <param name="reference">The name of the commit/branch/tag. Defaults to the repository's default branch if not specified.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The <see cref="Content"/> object representing the file or directory content, or <c>null</c> if not found.</returns>
//    public async Task<Content?> GetRepositoryContentAsync(string owner, string repo, string path, string? reference, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetRepositoryContentAsync(owner, repo, path, reference, cancellationToken);
//        return res.CastModel<Content>();
//    }

//    /// <summary>
//    /// Retrieves the content of a file in a repository as a string.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="path">The path to the file.</param>
//    /// <param name="reference">The name of the commit/branch/tag. Defaults to the repository's default branch if not specified.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The content of the file as a string, or <c>null</c> if not found.</returns>
//    public async Task<string?> GetRepositoryContentStringAsync(string owner, string repo, string path, string? reference, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetRepositoryContentStringAsync(owner, repo, path, reference, cancellationToken);
//        return res;
//    }

//    /// <summary>
//    /// Creates or updates the content of a file in a repository.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="path">The path to the file.</param>
//    /// <param name="message">The commit message.</param>
//    /// <param name="content">The new content of the file, encoded in Base64.</param>
//    /// <param name="sha">The blob SHA of the file being replaced. Required if updating an existing file.</param>
//    /// <param name="branch">The branch name where the changes will be made. Defaults to the repository's default branch if not specified.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The <see cref="ContentCommit"/> object representing the commit details, or <c>null</c> if the operation fails.</returns>
//    public async Task<ContentCommit?> CreateOrUpdateFileContentsAsync(string owner, string repo, string path, string message, string content, string? sha, string? branch, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.CreateOrUpdateFileContentsAsync(owner, repo, path, message, content, sha, branch, null, null, cancellationToken);
//        return res.CastModel<ContentCommit>();
//    }

//    /// <summary>
//    /// Deletes a file from a repository.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="path">The path to the file to delete.</param>
//    /// <param name="message">The commit message.</param>
//    /// <param name="sha">The blob SHA of the file to delete.</param>
//    /// <param name="branch">The branch name where the changes will be made. Defaults to the repository's default branch if not specified.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The <see cref="ContentCommit"/> object representing the commit details, or <c>null</c> if the operation fails.</returns>
//    public async Task<ContentCommit?> DeleteFileAsync(string owner, string repo, string path, string message, string? sha, string? branch, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.DeleteFileAsync(owner, repo, path, message, sha, branch, null, null, cancellationToken);
//        return res.CastModel<ContentCommit>();
//    }

//    #endregion

//    #region Tags

//    /// <summary>
//    /// Retrieves all tags for a specified repository.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>A collection of <see cref="Tag"/> objects if found; otherwise, <c>null</c>.</returns>
//    public async Task<IEnumerable<Tag>?> GetRepositoryTagsAsync(string owner, string repo, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetRepositoryTagsAsync(owner, repo, cancellationToken);
//        return res.CastModel<Tag>();
//    }

//    /// <summary>
//    /// Retrieves a specific tag by its name in a repository.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="tag">The name of the tag.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The <see cref="Tag"/> object if found; otherwise, <c>null</c>.</returns>
//    public async Task<Tag?> GetTagAsync(string owner, string repo, string tag, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetTagAsync(owner, repo, tag, cancellationToken);
//        return res.CastModel<Tag>();
//    }

//    /// <summary>
//    /// Retrieves all tag references for a specified repository.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>A collection of <see cref="Reference"/> objects representing tag references if found; otherwise, <c>null</c>.</returns>
//    public async Task<IEnumerable<Reference>?> GetTagsAsync(string owner, string repo, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetTagReferencesAsync(owner, repo, cancellationToken);
//        return res.CastModel<Reference>();
//    }

//    //public async Task<Reference?> GetTagAsync(string owner, string repo, string tagName, CancellationToken cancellationToken = default)
//    //{
//    //    WebServiceException.ThrowIfNullOrNotConnected(this.service);

//    //    var res = await service.GetTagAsync(owner, repo, tagName, cancellationToken);
//    //    return res.CastModel<Reference>();
//    //}

//    //public async Task<Tag> CreateTagAsync(string owner, string repo, string tagName, CancellationToken cancellationToken = default)
//    //{
//    //    WebServiceException.ThrowIfNullOrNotConnected(this.service);

//    //    var res = service.GetTagReferenceAsync(owner, repo, tagName, cancellationToken);
//    //    return res.CastModel<Tag>();
//    //}

//    //public async Task DeleteTagAsync(string owner, string repo, string tagName, CancellationToken cancellationToken = default)
//    //{
//    //    WebServiceException.ThrowIfNullOrNotConnected(this.service);

//    //    service.DeleteReferenceAsync(owner, repo, tag.Ref, cancellationToken);
//    //}

//    #endregion

//    #region Trees

//    /// <summary>
//    /// Retrieves a Git tree by its SHA hash.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="treeSha">The SHA hash of the tree to retrieve.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The <see cref="Tree"/> object if found; otherwise, <c>null</c>.</returns>
//    public async Task<Tree?> GetTreeAsync(string owner, string repo, string treeSha, CancellationToken cancellationToken = default)
//         => await GetTreeAsync(owner, repo, treeSha, false, cancellationToken);

//    /// <summary>
//    /// Retrieves a Git tree by its SHA hash, with an option to include subtrees recursively.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="treeSha">The SHA hash of the tree to retrieve.</param>
//    /// <param name="recursive">A value indicating whether to include subtrees recursively.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The <see cref="Tree"/> object if found; otherwise, <c>null</c>.</returns>
//    public async Task<Tree?> GetTreeAsync(string owner, string repo, string treeSha, bool recursive, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetTreeAsync(owner, repo, treeSha, recursive, cancellationToken);
//        return res.CastModel<Tree>();
//    }

//    //public async Task<Tree?> GetTreePathAsync(string owner, string repo, string treeSha, string path, CancellationToken cancellationToken = default)
//    //{
//    //    WebServiceException.ThrowIfNullOrNotConnected(this.service);

//    //    var res = await service.GetTreeAsync(owner, repo, treeSha, false, cancellationToken);

//    //    var pathItems = path.Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);
//    //    foreach (var pathItem in pathItems)
//    //    {
//    //        var sha = res?.Trees?.FirstOrDefault(t => string.Equals(t.Path, pathItem, StringComparison.OrdinalIgnoreCase))?.Sha;
//    //        if (sha == null) return null;

//    //        res = await service.GetTreeAsync(owner, repo, sha, false, cancellationToken);
//    //    }
//    //    return res.CastModel<Tree>();
//    //}

//    /// <summary>
//    /// Retrieves a Git tree for a specific path within a repository.
//    /// </summary>
//    /// <param name="owner">The owner of the repository.</param>
//    /// <param name="repo">The name of the repository.</param>
//    /// <param name="treeSha">The SHA hash of the tree to retrieve.</param>
//    /// <param name="path">The path within the tree to retrieve.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The <see cref="Tree"/> object representing the specified path if found; otherwise, <c>null</c>.</returns>
//    public async Task<Tree?> GetTreePathAsync(string owner, string repo, string treeSha, string path, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetTreeAsync(owner, repo, treeSha, true, cancellationToken);
//        if (res == null) return null;
//        if (res.Truncated)
//        {
//            // tree has more elements as GetTreeAsync can get, so step into path
//            var pathItems = path.Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);
//            foreach (var pathItem in pathItems)
//            {
//                var sha = res?.Trees?.FirstOrDefault(t => string.Equals(t.Path, pathItem, StringComparison.OrdinalIgnoreCase))?.Sha;
//                if (sha == null) return null;

//                res = await service.GetTreeAsync(owner, repo, sha, false, cancellationToken);
//            }
//            return res.CastModel<Tree>();
//        }
//        else
//        {
//            var list = res?.Trees?.ToList();
//            path = path.Replace('\\', '/').Trim('/') + '/';
//            int pathLength = path.Length;
//            var items = res?.Trees?.Where(i => i.Path!.StartsWith(path)).ToList();
//            items?.ForEach(i => i.Path = i.Path?.Substring(pathLength));
//            res!.Trees = items;
//            return res.CastModel<Tree>();
//        }
//    }

//    #endregion

//    #region User

//    /// <summary>
//    /// Retrieves the authenticated user's details.
//    /// </summary>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The <see cref="User"/> object representing the authenticated user, or <c>null</c> if not found.</returns>
//    public async Task<User?> GetAuthenticatedUserAsync(CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetAuthenticatedUserAsync(cancellationToken);
//        return res.CastModel<User>(); 
//    }

//    /// <summary>
//    /// Retrieves a user's details by their username.
//    /// </summary>
//    /// <param name="username">The username of the user.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The <see cref="User"/> object representing the user, or <c>null</c> if not found.</returns>
//    public async Task<User?> GetUserAsync(string username, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetUserAsync(username, cancellationToken);
//        return res.CastModel<User>();
//    }

//    /// <summary>
//    /// Retrieves a user's details by their unique ID.
//    /// </summary>
//    /// <param name="id">The unique ID of the user.</param>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>The <see cref="User"/> object representing the user, or <c>null</c> if not found.</returns>
//    public async Task<User?> GetUserAsync(long id, CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetUserAsync(id, cancellationToken);
//        return res.CastModel<User>();
//    }

//    #endregion

//    /// <summary>
//    /// Retrieves GitHub API metadata for the authenticated user or application.
//    /// </summary>
//    /// <param name="cancellationToken">A token to cancel the operation.</param>
//    /// <returns>A string containing the metadata information, or <c>null</c> if not available.</returns>
//    public async Task<Meta?> GetMetaAsync(CancellationToken cancellationToken = default)
//    {
//        WebServiceException.ThrowIfNullOrNotConnected(this.service);

//        var res = await service.GetMetaAsync(cancellationToken);
//        return res.CastModel<Meta>();
//    }
//}
