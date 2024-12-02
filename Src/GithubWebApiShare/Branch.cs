
namespace GithubWebApi;

[DebuggerDisplay("{Name}")]
public class Branch
{
    internal Branch(BranchModel model)
    {
        this.Name = model.Name;
        this.Commit = model.Commit.Facade<Commit>();
        this.Links = model.Links is not null ? new Links(model.Links) : null;
        this.Protected = model.Protected;
        //this.Protection = model.Protection is not null ? new Protection(model.Protection) : null;
        this.ProtectionUrl = model.ProtectionUrl;

        
    }

    

    public string? Name { get; }
    public Commit? Commit { get; }

    public Links? Links { get; }
    public bool? Protected { get; }

    public Protection? Protection { get; }
    public string? ProtectionUrl { get; }
}
