namespace GithubWebApiUnitTest;

[TestClass]
public partial class GithubTreesUnitTest : GithubBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetTreeAsync()
    {
        using var github = new Github(storeKey, appName);

        var root = await github.GetTreeAsync(testUser, testRepoFix, "BranchA");

        Assert.IsNotNull(root);
        Assert.IsNotNull(root.Trees);
        var list = root.Trees.ToList();

        var src = list.FirstOrDefault(t => t.Path == "Src");
        Assert.IsNotNull(src);
        Assert.IsNotNull(src.Sha);

        var folder = await github.GetTreeAsync(testUser, testRepoFix, src.Sha);
        Assert.IsNotNull(folder);

        //var branch = list.Single(b => b.Name == "BranchA");
        //Assert.IsNotNull(branch, nameof(branch));

        //Assert.AreEqual("BranchA", branch.Name, nameof(branch.Name));
        //Assert.IsNotNull(branch.Commit, nameof(branch.Commit));
        //Assert.IsNull(branch.Links, nameof(branch.Links));
        //Assert.AreEqual(false, branch.Protected, nameof(branch.Protected));
        //Assert.IsNull(branch.Protection, nameof(branch.Protection));
        //Assert.AreEqual($"{testHost.TrimEnd('/')}/repos/{testUser}/{testRepoFix}/branches/BranchA/protection", branch.ProtectionUrl, nameof(branch.ProtectionUrl));
    }
}
