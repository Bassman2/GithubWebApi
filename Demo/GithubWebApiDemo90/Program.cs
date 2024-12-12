using GithubWebApi;

namespace GithubWebApiDemo90;

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
            string? host = Environment.GetEnvironmentVariable("GITHUB_HOST");
            string? apiKey = Environment.GetEnvironmentVariable("GITHUB_APIKEY");


            using var github = new Github(apiKey!, "GithubWebApiDemo");

            var user = await github.GetAuthenticatedUserAsync();

            Console.WriteLine(user?.Login);
        }
        catch (Exception)
        { }

    }
}