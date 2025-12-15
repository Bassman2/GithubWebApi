namespace GithubWebApiUnitTest;

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
        Assert.IsFalse(branch.Protected, nameof(branch.Protected));
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
        Assert.IsFalse(branch.Protected, nameof(branch.Protected));
        Assert.IsNull(branch.Protection, nameof(branch.Protection));
        Assert.AreEqual($"{testHost.TrimEnd('/')}/repos/{testUser}/{testRepoFix}/branches/BranchA/protection", branch.ProtectionUrl, nameof(branch.ProtectionUrl));
    }

    [TestMethod]
    public async Task TestMethodCreateBranchFromMainAsync()
    {
        using var github = new Github(storeKey, appName);

        var reference = await github.CreateBranchAsync(testUser, testRepoDyn, "Branch1");

        Assert.IsNotNull(reference, nameof(reference));

        Assert.AreEqual("refs/heads/Branch1", reference.Ref, nameof(reference.Ref));
        Assert.IsNotNull(reference.NodeId, nameof(reference.NodeId));
        Assert.AreEqual("https://api.github.com/repos/Bassman2/WS-Test-Dyn/git/refs/heads/Branch1", reference.Url, nameof(reference.Url));
        Assert.IsNotNull(reference.Object, nameof(reference.Object));
        Assert.IsNotNull(reference.Object.Sha, nameof(reference.Object.Sha));
        Assert.AreEqual("commit", reference.Object.Type, nameof(reference.Object.Type));
        Assert.IsNotNull(reference.Object.Url, nameof(reference.Object.Url));

        await github.DeleteBranchAsync(testUser, testRepoDyn, "Branch1");
    }

    [TestMethod]
    public async Task TestMethodCreateBranchFromOtherAsync()
    {
        using var github = new Github(storeKey, appName);

        var reference = await github.CreateBranchAsync(testUser, testRepoDyn, "BranchA", "Branch2");

        Assert.IsNotNull(reference, nameof(reference));

        Assert.AreEqual("refs/heads/Branch2", reference.Ref, nameof(reference.Ref));
        Assert.IsNotNull(reference.NodeId, nameof(reference.NodeId));
        Assert.AreEqual("https://api.github.com/repos/Bassman2/WS-Test-Dyn/git/refs/heads/Branch2", reference.Url, nameof(reference.Url));
        Assert.IsNotNull(reference.Object, nameof(reference.Object));
        Assert.IsNotNull(reference.Object.Sha, nameof(reference.Object.Sha));
        Assert.AreEqual("commit", reference.Object.Type, nameof(reference.Object.Type));
        Assert.IsNotNull(reference.Object.Url, nameof(reference.Object.Url));

        await github.DeleteBranchAsync(testUser, testRepoDyn, "Branch2");
    }
}
