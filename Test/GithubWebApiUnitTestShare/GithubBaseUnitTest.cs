namespace GithubWebApiUnitTest;

public abstract class GithubBaseUnitTest
{
    protected static readonly CultureInfo culture = new CultureInfo("en-US");

    protected const string storeKey = "github";

    protected const string appName = "UnitTest";

    protected static readonly string testHost = KeyStore.Key(storeKey)!.Host!;
    protected static readonly string testUserKey = KeyStore.Key(storeKey)!.Login!;
    protected static readonly string testUserDisplayName = KeyStore.Key(storeKey)!.User!;
    protected static readonly string testUserEmail = KeyStore.Key(storeKey)!.Email!;
        
    protected const string testUser = "Bassman2";
    protected const string testRepoFix = "WS-Test-Fix";
    protected const string testRepoDyn = "WS-Test-Dyn";
    protected const string otherUser = "chcg";
    protected const string mainBranch = "main";
}
