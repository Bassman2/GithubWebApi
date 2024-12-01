namespace GithubWebApi;

[DebuggerDisplay("{Name}")]
public class Branch
{
    internal Branch(BranchModel model)
    {
        this.Name = model.Name;
        this.Protected = model.Protected;
    }

    public string? Name { get; }

    public bool? Protected { get; }

    public string? ProtectionUrl { get; }
}
