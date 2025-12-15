namespace GithubWebApiUnitTest;

[TestClass]
public partial class GithubUsersUnitTest : GithubBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetAuthenticatedUserAsync()
    {
        using var github = new Github(storeKey, appName);

        User? user = await github.GetAuthenticatedUserAsync();

        Assert.IsNotNull(user);
        Assert.AreEqual(testUser, user.Login, nameof(user.Login));
        Assert.AreEqual(29576084, user.Id, nameof(user.Id));
        Assert.AreEqual("MDQ6VXNlcjI5NTc2MDg0", user.NodeId, nameof(user.NodeId));
        Assert.AreEqual("https://avatars.githubusercontent.com/u/29576084?v=4", user.AvatarUrl, nameof(user.AvatarUrl));

        Assert.AreEqual("User", user.Type, nameof(user.Type));
        Assert.IsFalse(user.SiteAdmin, nameof(user.SiteAdmin));
        Assert.AreEqual("Ralf Beckers", user.Name, nameof(user.Name));
        Assert.IsNull(user.Company, nameof(user.Company));
        Assert.AreEqual("Germany", user.Location, nameof(user.Location));
        Assert.AreEqual("ralf.beckers@hotmail.com", user.Email, nameof(user.Email));


    }

    [TestMethod]
    public async Task TestMethodGetUserNameAsync()
    {
        using var github = new Github(storeKey, appName);

        User? user = await github.GetUserAsync(otherUser);

        Assert.IsNotNull(user);
        Assert.AreEqual(otherUser, user.Login, nameof(user.Login));
        Assert.AreEqual(12630740, user.Id, nameof(user.Id));
        Assert.AreEqual("MDQ6VXNlcjEyNjMwNzQw", user.NodeId, nameof(user.NodeId));
        Assert.AreEqual("https://avatars.githubusercontent.com/u/12630740?v=4", user.AvatarUrl, nameof(user.AvatarUrl));

        Assert.AreEqual("User", user.Type, nameof(user.Type));
        Assert.IsFalse(user.SiteAdmin, nameof(user.SiteAdmin));
        Assert.IsNull(user.Name, nameof(user.Name));
        Assert.IsNull(user.Company, nameof(user.Company));
        Assert.IsNull(user.Location, nameof(user.Location));
        Assert.IsNull(user.Email, nameof(user.Email));
    }

    [TestMethod]
    public async Task TestMethodGetUserIdAsync()
    {
        using var github = new Github(storeKey, appName);

        User? user = await github.GetUserAsync(12630740);

        Assert.IsNotNull(user);
        Assert.AreEqual("chcg", user.Login, nameof(user.Login));
        Assert.AreEqual(12630740, user.Id, nameof(user.Id));
        Assert.AreEqual("MDQ6VXNlcjEyNjMwNzQw", user.NodeId, nameof(user.NodeId));
        Assert.AreEqual("https://avatars.githubusercontent.com/u/12630740?v=4", user.AvatarUrl, nameof(user.AvatarUrl));

        Assert.AreEqual("User", user.Type, nameof(user.Type));
        Assert.IsFalse(user.SiteAdmin, nameof(user.SiteAdmin));
        Assert.IsNull(user.Name, nameof(user.Name));
        Assert.IsNull(user.Company, nameof(user.Company));
        Assert.IsNull(user.Location, nameof(user.Location));
        Assert.IsNull(user.Email, nameof(user.Email));
    }

    [TestMethod]
    public async Task TestMethodGetMetaAsync()
    {
        using var github = new Github(storeKey, appName);

        var meta = await github.GetMetaAsync();

        Assert.IsNotNull(meta);
        Assert.AreEqual("3.14.11", meta.InstalledVersion);

    }
}
