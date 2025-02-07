﻿namespace GithubWebApiUnitTest;

[TestClass]
public partial class GithubRepositoriesUnitTest : GithubBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetUserEmptyRepositoriesAsync()
    {
        using var github = new Github(storeKey, appName);

        var repos = github.GetUserRepositoriesAsync("Bassman");

        Assert.IsNotNull(repos);

        var list = await repos.ToListAsync();
        var repo = await repos.FirstOrDefaultAsync();

        Assert.AreEqual(0, list.Count, nameof(list.Count));
        Assert.IsNull(repo);
    }

    [TestMethod]
    public async Task TestMethodGetUserRepositoriesAsync()
    {
        using var github = new Github(storeKey, appName);

        var repos = github.GetUserRepositoriesAsync(otherUser);

        Assert.IsNotNull(repos);
        var repo = await repos.FirstOrDefaultAsync(r => r.Name == "MediaDevices");

        Assert.IsNotNull(repo);
        Assert.AreEqual(109999901, repo.Id, nameof(repo.Id));
        Assert.AreEqual("MDEwOlJlcG9zaXRvcnkxMDk5OTk5MDE=", repo.NodeId, nameof(repo.NodeId));
        Assert.AreEqual("MediaDevices", repo.Name, nameof(repo.Name));
        Assert.AreEqual("chcg/MediaDevices", repo.FullName, nameof(repo.FullName));

        Assert.AreEqual(false, repo.Private, nameof(repo.Private));
        Assert.AreEqual("MTP Library", repo.Description, nameof(repo.Description));
        Assert.AreEqual(true, repo.Fork, nameof(repo.Fork));
        Assert.AreEqual("https://api.github.com/repos/chcg/MediaDevices", repo.Url, nameof(repo.Url));
        Assert.AreEqual("https://api.github.com/repos/chcg/MediaDevices/{archive_format}{/ref}", repo.ArchiveUrl, nameof(repo.ArchiveUrl));
    }

    [TestMethod]
    public async Task TestMethodGetRepositoryAsync()
    {
        using var github = new Github(storeKey, appName);

        var repo = await github.GetRepositoryAsync(testUser, testRepoFix);

        Assert.IsNotNull(repo);
        Assert.AreEqual(915278053, repo.Id, nameof(repo.Id));
        Assert.AreEqual("R_kgDONo4I5Q", repo.NodeId, nameof(repo.NodeId));
        Assert.AreEqual(testRepoFix, repo.Name, nameof(repo.Name));
        Assert.AreEqual($"{testUser}/{testRepoFix}", repo.FullName, nameof(repo.FullName));

        Assert.AreEqual(false, repo.Private, nameof(repo.Private));
        Assert.AreEqual("Test repository for fix tests", repo.Description, nameof(repo.Description));
        Assert.AreEqual(false, repo.Fork, nameof(repo.Fork));
        Assert.AreEqual($"{testHost.TrimEnd('/')}/repos/{testUser}/{testRepoFix}", repo.Url, nameof(repo.Url));
        Assert.AreEqual($"{testHost.TrimEnd('/')}/repos/{testUser}/{testRepoFix}/{{archive_format}}{{/ref}}", repo.ArchiveUrl, nameof(repo.ArchiveUrl));
    }
}
