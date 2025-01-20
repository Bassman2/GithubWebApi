namespace GithubWebApi;

public sealed class Github : IDisposable
{
    private GithubService? service;

    public Github(string storeKey, string appName)
        : this(new Uri(KeyStore.Key(storeKey)?.Host!), KeyStore.Key(storeKey)!.Token!, appName)
    { }

    public Github(Uri host, string token, string appName)
    {
        service = new GithubService(host, new BearerAuthenticator(token), appName);
    }

    public void Dispose()
    {
        if (this.service != null)
        {
            this.service.Dispose();
            this.service = null;
        }
        GC.SuppressFinalize(this);
    }

    //public async Task<BranchModel?> GetHeadRevisionAsync(string owner, string repo, CancellationToken cancellationToken = default)
    //{
    //    WebServiceException.ThrowIfNullOrNotConnected(this.service);

    //    var res = await service.GetHeadRevisionAsync(owner, repo, cancellationToken);
    //    return res; //?.Select(p => new PullRequest(p)).ToList();
    //}

    #region Branches

    public async IAsyncEnumerable<Branch> GetBranchesAsync(string owner, string repo, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = service.GetBranchesAsync(owner, repo, cancellationToken);
        await foreach (var item in res)
        {
            yield return new Branch(item);
        }
    }

    public async Task<Branch?> GetBranchAsync(string owner, string repo, string branch, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetBranchAsync(owner, repo, branch, cancellationToken);
        return res is not null ? new Branch(res) : null;
    }

    public async Task<Branch?> RenameBranchAsync(string owner, string repo, string branch, string newName, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);
                
        var res = await service.RenameBranchAsync(owner, repo, branch, newName, cancellationToken);
        return res is not null ? new Branch(res) : null;
    }

    public async Task<Reference?> CreateBranchAsync(string owner, string repo, string newBranchName, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var refs = await service.GetHeadReferencesAsync(owner, repo, cancellationToken);
        string sha = refs!.Last().Object!.Sha!;

        var res = await service.CreateHeadReferenceAsync(owner, repo, sha, newBranchName, cancellationToken);
        return res.CastModel<Reference>();
    }

    public async Task<Reference?> CreateBranchAsync(string owner, string repo, string branchName, string newBranchName, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var branch = await service.GetHeadReferenceAsync(owner, repo, branchName, cancellationToken) ?? throw new ArgumentException("Branch not found", nameof(branchName));

        //var refs = await service.GetHeadReferencesAsync(owner, repo, cancellationToken);
        //string? sha = refs?.First(r => r.Ref?.Substring(r.Ref.LastIndexOf('/') + 1) == branch)?.Object!.Sha;
        //if (sha == null) throw new ArgumentException(branch, nameof(branch));

        var res = await service.CreateHeadReferenceAsync(owner, repo, branch.Object!.Sha!, newBranchName, cancellationToken);
        return res.CastModel<Reference>();
    }

    public async Task DeleteBranchAsync(string owner, string repo, string branchName, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var branch = await service.GetHeadReferenceAsync(owner, repo, branchName, cancellationToken) ?? throw new ArgumentException("Branch not found", nameof(branchName));
        await service.DeleteReferenceAsync(owner, repo, branch.Ref!, cancellationToken);
    }
    
    #endregion

    #region Pull Request

    public async Task<IEnumerable<PullRequest>?> GetPullsAsync(string owner, string repo, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetPullsAsync(owner, repo, cancellationToken);
        return res?.Select(p => new PullRequest(p)).ToList();
    }

    public async Task<PullRequest?> CreatePullAsync(string owner, string repo, string title, string body, string head, string baseBranch, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.CreatePullAsync(owner, repo, title, body, head, baseBranch, cancellationToken);
        return res is not null ? new PullRequest(res) : null;
    }

    public async Task<PullRequest?> GetPullAsync(string owner, string repo, int pullNumber, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetPullAsync(owner, repo, pullNumber, cancellationToken);
        return res is not null ? new PullRequest(res) : null; 
    }

    public async Task<PullRequest?> UpdatePullAsync(string owner, string repo, int pullNumber, string title, string body, string state, string baseBranch, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.UpdatePullAsync(owner, repo, pullNumber, title, body, state, baseBranch, cancellationToken);
        return res is not null ? new PullRequest(res) : null; 
    }

    #endregion

    #region Release

    public async Task<Release?> CreateReleaseAsync(string owner, string repo, object tag, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.CreateReleaseAsync(owner, repo, tag, cancellationToken);
        return res.CastModel<Release>();
    }

    #endregion

    #region Repositories

    public async IAsyncEnumerable<Repository> GetOrganizationRepositoriesAsync(string org, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = service.GetOrganizationRepositoriesAsync(org, cancellationToken);
        if (res is not null)
        {
            await foreach (var item in res)
            {
                yield return item.CastModel<Repository>()!;
            }
        }
    }

    public async IAsyncEnumerable<Repository> GetAuthenticatedUserRepositoriesAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = service.GetAuthenticatedUserRepositoriesAsync(cancellationToken);
        if (res is not null)
        {
            await foreach (var item in res)
            {
                yield return item.CastModel<Repository>()!;
            }
        }
    }

    public async IAsyncEnumerable<Repository> GetUserRepositoriesAsync(string user, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = service.GetUserRepositoriesAsync(user, cancellationToken);
        if (res is not null)
        {
            await foreach (var item in res)
            {
                yield return item.CastModel<Repository>()!; 
            }
        }
    }

    public async IAsyncEnumerable<Repository> GetPublicRepositoriesAsync(int since, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = service.GetPublicRepositoriesAsync(since, cancellationToken);
        if (res is not null)
        {
            await foreach (var item in res)
            {
                yield return item.CastModel<Repository>()!;
            }
        }
    }

    public async Task<Repository?> GetRepositoryAsync(string owner, string repo, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetRepositoryAsync(owner, repo, cancellationToken);
        return res.CastModel<Repository>();  
    }



    #endregion

    #region Repository Contents

    public async Task<Content?> GetRepositoryContentAsync(string owner, string repo, string path, string? reference, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetRepositoryContentAsync(owner, repo, path, reference, cancellationToken);
        return res.CastModel<Content>();
    }

    public async Task<string?> GetRepositoryContentStringAsync(string owner, string repo, string path, string? reference, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetRepositoryContentStringAsync(owner, repo, path, reference, cancellationToken);
        return res;
    }

    public async Task<ContentCommit?> CreateOrUpdateFileContentsAsync(string owner, string repo, string path, string message, string content, string? sha, string? branch, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.CreateOrUpdateFileContentsAsync(owner, repo, path, message, content, sha, branch, null, null, cancellationToken);
        return res.CastModel<ContentCommit>();
    }

    public async Task<ContentCommit?> DeleteFileAsync(string owner, string repo, string path, string message, string? sha, string? branch,CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.DeleteFileAsync(owner, repo, path, message, sha, branch, null, null, cancellationToken);
        return res.CastModel<ContentCommit>();
    }

    #endregion

    #region Tags

    public async Task<IEnumerable<Tag>?> GetRepositoryTagsAsync(string owner, string repo, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetRepositoryTagsAsync(owner, repo, cancellationToken);
        return res.CastModel<Tag>();
    }

    public async Task<Tag?> GetTagAsync(string owner, string repo, string tag, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetTagAsync(owner, repo, tag, cancellationToken);
        return res.CastModel<Tag>();
    }



    public async Task<IEnumerable<Reference>?> GetTagsAsync(string owner, string repo, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetTagReferencesAsync(owner, repo, cancellationToken);
        return res.CastModel<Reference>();
    }

    //public async Task<Reference?> GetTagAsync(string owner, string repo, string tagName, CancellationToken cancellationToken = default)
    //{
    //    WebServiceException.ThrowIfNullOrNotConnected(this.service);

    //    var res = await service.GetTagAsync(owner, repo, tagName, cancellationToken);
    //    return res.CastModel<Reference>();
    //}

    //public async Task<Tag> CreateTagAsync(string owner, string repo, string tagName, CancellationToken cancellationToken = default)
    //{
    //    WebServiceException.ThrowIfNullOrNotConnected(this.service);

    //    var res = service.GetTagReferenceAsync(owner, repo, tagName, cancellationToken);
    //    return res.CastModel<Tag>();
    //}

    //public async Task DeleteTagAsync(string owner, string repo, string tagName, CancellationToken cancellationToken = default)
    //{
    //    WebServiceException.ThrowIfNullOrNotConnected(this.service);

    //    service.DeleteReferenceAsync(owner, repo, tag.Ref, cancellationToken);
    //}

    #endregion

    #region Trees

    public async Task<Tree?> GetTreeAsync(string owner, string repo, string treeSha, CancellationToken cancellationToken = default)
         => await GetTreeAsync(owner, repo, treeSha, false, cancellationToken);
    

    public async Task<Tree?> GetTreeAsync(string owner, string repo, string treeSha, bool recursive, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetTreeAsync(owner, repo, treeSha, recursive, cancellationToken);
        return res.CastModel<Tree>();
    }

    //public async Task<Tree?> GetTreePathAsync(string owner, string repo, string treeSha, string path, CancellationToken cancellationToken = default)
    //{
    //    WebServiceException.ThrowIfNullOrNotConnected(this.service);

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

    public async Task<Tree?> GetTreePathAsync(string owner, string repo, string treeSha, string path, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetTreeAsync(owner, repo, treeSha, true, cancellationToken);
        if (res == null) return null;
        if (res.Truncated)
        {
            // tree has more elements as GetTreeAsync can get, so step into path
            var pathItems = path.Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);
            foreach (var pathItem in pathItems)
            {
                var sha = res?.Trees?.FirstOrDefault(t => string.Equals(t.Path, pathItem, StringComparison.OrdinalIgnoreCase))?.Sha;
                if (sha == null) return null;

                res = await service.GetTreeAsync(owner, repo, sha, false, cancellationToken);
            }
            return res.CastModel<Tree>();
        }
        else
        {
            var list = res?.Trees?.ToList();
            path = path.Replace('\\', '/').Trim('/') + '/';
            int pathLength = path.Length;
            var items = res?.Trees?.Where(i => i.Path!.StartsWith(path)).ToList();
            items?.ForEach(i => i.Path = i.Path?.Substring(pathLength));
            res!.Trees = items;
            return res.CastModel<Tree>();
        }
    }

    #endregion

    #region User

    public async Task<User?> GetAuthenticatedUserAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetAuthenticatedUserAsync(cancellationToken);
        return res.CastModel<User>(); 
    }

    public async Task<User?> GetUserAsync(string username, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetUserAsync(username, cancellationToken);
        return res.CastModel<User>();
    }

    public async Task<User?> GetUserAsync(long id, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetUserAsync(id, cancellationToken);
        return res.CastModel<User>();
    }

    #endregion
}
