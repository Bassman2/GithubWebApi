﻿namespace GithubWebApi;

public sealed class Github : IDisposable
{
    private const string defAppName = "GithubWebApi";

    private GithubService? service;

    public Github(string apiKey, string appName = defAppName)
    {
        service = new GithubService(apiKey, appName);
    }

    public Github(Uri host, string apiKey, string appName = defAppName)
    {
        service = new GithubService(host, apiKey, appName);
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

    public async Task<IEnumerable<Branch>?> GetBranchesAsync(string owner, string repo, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetBranchesAsync(owner, repo, cancellationToken);
        return res?.Select(i => new Branch(i)).ToList();
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

    #region Repositories

    public async IAsyncEnumerable<Repository> GetOrganizationRepositoriesAsync(string org, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = service.GetOrganizationRepositoriesAsync(org, cancellationToken);
        if (res is not null)
        {
            await foreach (var item in res)
            {
                yield return new Repository(item);
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
                yield return new Repository(item);
            }
        }
    }

    public async IAsyncEnumerable<Repository> GetUserRepositoriesAsync(string user, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = service.GetUserRepositoriesAsync(user, cancellationToken);
        //if (res is not null)
        {
            await foreach (var item in res)
            {
                yield return new Repository(item);
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
                yield return new Repository(item);
            }
        }
    }

    public async Task<Repository?> GetRepositoryAsync(string owner, string repo, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetRepositoryAsync(owner, repo, cancellationToken);
        return res is not null ? new Repository(res) : null;
    }

    #endregion

    #region User

    public async Task<User?> GetAuthenticatedUserAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetAuthenticatedUserAsync(cancellationToken);
        return res is not null ? new User(res) : null;
    }

    public async Task<User?> GetUserAsync(string username, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetUserAsync(username, cancellationToken);
        return res is not null ? new User(res) : null;
    }

    public async Task<User?> GetUserAsync(long id, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetUserAsync(id, cancellationToken);
        return res is not null ? new User(res) : null;
    }
    #endregion
}
