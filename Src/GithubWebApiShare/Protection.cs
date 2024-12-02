namespace GithubWebApi;

public class Protection
{
    internal Protection()
    { }

    public bool? Enabled { get; set; }

    public RequiredStatusChecks? RequiredStatusChecks { get; set; }
}
