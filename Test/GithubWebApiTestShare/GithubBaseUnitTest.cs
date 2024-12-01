namespace GithubWebApiTest
{
    public abstract class GithubBaseUnitTest
    {
        protected static readonly string host = Environment.GetEnvironmentVariable("GITHUB_HOST")!;
        protected static readonly string apiKey = apiKey = Environment.GetEnvironmentVariable("GITHUB_APIKEY")!;
        protected const string testUser = "Bassman2";
        protected const string testRepo = "ApiTest";
        protected const string otherUser = "chcg";
    }
}
