namespace GithubWebApiTest;

public partial class GithubTest
{
    [TestMethod]
    public async Task TestMethodGetAuthenticatedUserAsync()
    {
        using var github = new Github(apiKey!);

        User? user = await github.GetAuthenticatedUserAsync();

        Assert.IsNotNull(user);
        Assert.AreEqual("Bassman2", user.Login, nameof(user.Login));
        Assert.AreEqual(29576084, user.Id, nameof(user.Id));
        Assert.AreEqual("MDQ6VXNlcjI5NTc2MDg0", user.NodeId, nameof(user.NodeId));
        Assert.AreEqual("https://avatars.githubusercontent.com/u/29576084?v=4", user.AvatarUrl, nameof(user.AvatarUrl));

        Assert.AreEqual("User", user.Type, nameof(user.Type));
        Assert.AreEqual(false, user.SiteAdmin, nameof(user.SiteAdmin));
        Assert.AreEqual("Ralf Beckers", user.Name, nameof(user.Name));
        Assert.AreEqual(null, user.Company, nameof(user.Company));
        Assert.AreEqual("Germany", user.Location, nameof(user.Location));
        Assert.AreEqual("ralf.beckers@hotmail.com", user.Email, nameof(user.Email));


    }

    [TestMethod]
    public async Task TestMethodGetUserNameAsync()
    {
        using var github = new Github(apiKey!);

        User? user = await github.GetUserAsync("chcg");

        Assert.IsNotNull(user);
        Assert.AreEqual("chcg", user.Login, nameof(user.Login));
        Assert.AreEqual(12630740, user.Id, nameof(user.Id));
        Assert.AreEqual("MDQ6VXNlcjEyNjMwNzQw", user.NodeId, nameof(user.NodeId));
        Assert.AreEqual("https://avatars.githubusercontent.com/u/12630740?v=4", user.AvatarUrl, nameof(user.AvatarUrl));

        Assert.AreEqual("User", user.Type, nameof(user.Type));
        Assert.AreEqual(false, user.SiteAdmin, nameof(user.SiteAdmin));
        Assert.AreEqual(null, user.Name, nameof(user.Name));
        Assert.AreEqual(null, user.Company, nameof(user.Company));
        Assert.AreEqual(null, user.Location, nameof(user.Location));
        Assert.AreEqual(null, user.Email, nameof(user.Email));
    }

    [TestMethod]
    public async Task TestMethodGetUserIdAsync()
    {
        using var github = new Github(apiKey!);

        User? user = await github.GetUserAsync(12630740);

        Assert.IsNotNull(user);
        Assert.AreEqual("chcg", user.Login, nameof(user.Login));
        Assert.AreEqual(12630740, user.Id, nameof(user.Id));
        Assert.AreEqual("MDQ6VXNlcjEyNjMwNzQw", user.NodeId, nameof(user.NodeId));
        Assert.AreEqual("https://avatars.githubusercontent.com/u/12630740?v=4", user.AvatarUrl, nameof(user.AvatarUrl));

        Assert.AreEqual("User", user.Type, nameof(user.Type));
        Assert.AreEqual(false, user.SiteAdmin, nameof(user.SiteAdmin));
        Assert.AreEqual(null, user.Name, nameof(user.Name));
        Assert.AreEqual(null, user.Company, nameof(user.Company));
        Assert.AreEqual(null, user.Location, nameof(user.Location));
        Assert.AreEqual(null, user.Email, nameof(user.Email));
    }

}
