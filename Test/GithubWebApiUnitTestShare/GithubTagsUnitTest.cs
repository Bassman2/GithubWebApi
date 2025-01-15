namespace GithubWebApiUnitTest;

[TestClass]
public partial class GithubTagsUnitTestt : GithubBaseUnitTest
{

    [TestMethod]
    public async Task TestMethodGetRepositoryTagsAsync()
    {
        using var github = new Github(storeKey, appName);

        var tags = await github.GetRepositoryTagsAsync(testUser, testRepoFix);
        Assert.IsNotNull(tags);


    }

    [TestMethod]
    public async Task TestMethodGetTagAsync()
    {
        using var github = new Github(storeKey, appName);

        var tags = await github.GetTagAsync(testUser, testRepoFix, "");
        Assert.IsNotNull(tags);


    }

    [TestMethod]
    public async Task TestMethodGetTagsAsync()
    {
        using var github = new Github(storeKey, appName);

        var tags = await github.GetTagsAsync(testUser, testRepoFix);

        Assert.IsNotNull(tags);
        var list = tags.ToList();

        //var branch = list.Single(b => b.Name == "BranchA");
        //Assert.IsNotNull(branch, nameof(branch));

        //Assert.AreEqual("BranchA", branch.Name, nameof(branch.Name));
        //Assert.IsNotNull(branch.Commit, nameof(branch.Commit));
        //Assert.IsNull(branch.Links, nameof(branch.Links));
        //Assert.AreEqual(false, branch.Protected, nameof(branch.Protected));
        //Assert.IsNull(branch.Protection, nameof(branch.Protection));
        //Assert.AreEqual($"{testHost.TrimEnd('/')}/repos/{testUser}/{testRepoFix}/branches/BranchA/protection", branch.ProtectionUrl, nameof(branch.ProtectionUrl));
    }

//    [TestMethod]
//    public async Task TestMethodGetTagAsync()
//    {
//        using var github = new Github(storeKey, appName);

//        var branches = github.GetTagAsync(testUser, testRepoFix);

//        Assert.IsNotNull(branches);
//        var list = await branches.ToListAsync();

//        var branch = list.Single(b => b.Name == "BranchA");
//        Assert.IsNotNull(branch, nameof(branch));

//        Assert.AreEqual("BranchA", branch.Name, nameof(branch.Name));
//        Assert.IsNotNull(branch.Commit, nameof(branch.Commit));
//        Assert.IsNull(branch.Links, nameof(branch.Links));
//        Assert.AreEqual(false, branch.Protected, nameof(branch.Protected));
//        Assert.IsNull(branch.Protection, nameof(branch.Protection));
//        Assert.AreEqual($"{testHost.TrimEnd('/')}/repos/{testUser}/{testRepoFix}/branches/BranchA/protection", branch.ProtectionUrl, nameof(branch.ProtectionUrl));
//    }

//    [TestMethod]
//    public async Task TestMethodCreateTagAsync()
//    {
//        using var github = new Github(storeKey, appName);

//        var branches = github.GetBranchesAsync(testUser, testRepoFix);

//        Assert.IsNotNull(branches);
//        var list = await branches.ToListAsync();

//        var branch = list.Single(b => b.Name == "BranchA");
//        Assert.IsNotNull(branch, nameof(branch));

//        Assert.AreEqual("BranchA", branch.Name, nameof(branch.Name));
//        Assert.IsNotNull(branch.Commit, nameof(branch.Commit));
//        Assert.IsNull(branch.Links, nameof(branch.Links));
//        Assert.AreEqual(false, branch.Protected, nameof(branch.Protected));
//        Assert.IsNull(branch.Protection, nameof(branch.Protection));
//        Assert.AreEqual($"{testHost.TrimEnd('/')}/repos/{testUser}/{testRepoFix}/branches/BranchA/protection", branch.ProtectionUrl, nameof(branch.ProtectionUrl));
//    }

}
