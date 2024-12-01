namespace GithubWebApiTest;

[TestClass]
public partial class GithubBranchesUnitTest :  GithubBaseUnitTest
{
    

    [TestMethod]
    public async Task TestMethodGetBranchesAsync()
    {
        using var github = new Github(apiKey);

        var branches = github.GetBranchesAsync(testUser, testRepo);

        Assert.IsNotNull(branches);
        var list = await branches.ToListAsync();
        //var repo = await repos.FirstOrDefaultAsync(r => r.Name == "MediaDevices");

        //Assert.IsNotNull(repo);
        //Assert.AreEqual(109999901, repo.Id, nameof(repo.Id));
        //Assert.AreEqual("MDEwOlJlcG9zaXRvcnkxMDk5OTk5MDE=", repo.NodeId, nameof(repo.NodeId));
        //Assert.AreEqual("MediaDevices", repo.Name, nameof(repo.Name));
        //Assert.AreEqual("chcg/MediaDevices", repo.FullName, nameof(repo.FullName));

        //Assert.AreEqual(false, repo.Private, nameof(repo.Private));
        //Assert.AreEqual("MTP Library", repo.Description, nameof(repo.Description));
        //Assert.AreEqual(true, repo.Fork, nameof(repo.Fork));
        //Assert.AreEqual("https://api.github.com/repos/chcg/MediaDevices", repo.Url, nameof(repo.Url));
        //Assert.AreEqual("https://api.github.com/repos/chcg/MediaDevices/{archive_format}{/ref}", repo.ArchiveUrl, nameof(repo.ArchiveUrl));
    }

    [TestMethod]
    public async Task TestMethodGetBranchAsync()
    {
        using var github = new Github(apiKey!);

        var branch = await github.GetBranchAsync(testUser, testRepo, mainBranch);

        Assert.IsNotNull(branch);
        Assert.AreEqual(mainBranch, branch.Name, nameof(branch.Name));
    }
}
