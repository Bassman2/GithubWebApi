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
        Assert.AreEqual(4, list.Count, nameof(list.Count));

        var src = list.FirstOrDefault(t => t.Path == "Src");
        Assert.IsNotNull(src);
        Assert.IsNotNull(src.Sha);

        var folder = await github.GetTreeAsync(testUser, testRepoFix, src.Sha);
        Assert.IsNotNull(folder);
        Assert.IsNotNull(folder.Trees);
        var folders = folder.Trees.ToList();

        Assert.AreEqual(3, folders.Count, nameof(folders.Count));
    }

    [TestMethod]
    public async Task TestMethodGetTreeRecursiveAsync()
    {
        using var github = new Github(storeKey, appName);

        var root = await github.GetTreeAsync(testUser, testRepoFix, "BranchA", true);

        Assert.IsNotNull(root);
        Assert.IsNotNull(root.Trees);
        var list = root.Trees.ToList();
        Assert.AreEqual(16, list.Count, nameof(list.Count));
    }
}
