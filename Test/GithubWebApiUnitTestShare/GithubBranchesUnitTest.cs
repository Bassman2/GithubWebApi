namespace GithubWebApiTest;

[TestClass]
public partial class GithubBranchesUnitTest :  GithubBaseUnitTest
{
    

    [TestMethod]
    public async Task TestMethodGetBranchesAsync()
    {
        using var github = new Github(storeKey, appName);

        var branches = github.GetBranchesAsync(testUser, testRepoFix);

        Assert.IsNotNull(branches);
        var list = await branches.ToListAsync();

        var branch = list.Single(b => b.Name == "BranchA");
        Assert.IsNotNull(branch, nameof(branch));

        Assert.AreEqual("BranchA", branch.Name, nameof(branch.Name));
        Assert.IsNotNull(branch.Commit, nameof(branch.Commit));
        Assert.IsNull(branch.Links, nameof(branch.Links));
        Assert.AreEqual(false, branch.Protected, nameof(branch.Protected));
        Assert.IsNull(branch.Protection, nameof(branch.Protection));
        Assert.AreEqual($"{testHost.TrimEnd('/')}/repos/{testUser}/{testRepoFix}/branches/BranchA/protection", branch.ProtectionUrl, nameof(branch.ProtectionUrl));
    }

    [TestMethod]
    public async Task TestMethodGetBranchAsync()
    {
        using var github = new Github(storeKey, appName);

        var branch = await github.GetBranchAsync(testUser, testRepoFix, "BranchA");

        Assert.IsNotNull(branch, nameof(branch));

        Assert.AreEqual("BranchA", branch.Name, nameof(branch.Name));
        Assert.IsNotNull(branch.Commit, nameof(branch.Commit));
        Assert.IsNotNull(branch.Links, nameof(branch.Links));
        Assert.AreEqual(false, branch.Protected, nameof(branch.Protected));
        Assert.IsNull(branch.Protection, nameof(branch.Protection));
        Assert.AreEqual($"{testHost.TrimEnd('/')}/repos/{testUser}/{testRepoFix}/branches/BranchA/protection", branch.ProtectionUrl, nameof(branch.ProtectionUrl));
    }
}
