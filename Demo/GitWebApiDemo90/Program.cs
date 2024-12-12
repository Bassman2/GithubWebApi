using GithubWebApi;

namespace GitWebApiDemo90;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        new Program().Test();
    }

    public void Test()
    {
        Task.Run(async () => { await TestAsync(); }).Wait();

    }

    public async Task TestAsync()
    {
        try
        {
            string? host = Environment.GetEnvironmentVariable("GIT_HOST");
            string? apiKey = Environment.GetEnvironmentVariable("GIT_APIKEY");

            using var github = new Github(new Uri(host!), apiKey!, "GitWebApiDemo");

            var user = await github.GetAuthenticatedUserAsync();

            Console.WriteLine(user?.Login);
        }
        catch (Exception)
        { }
    }
}
