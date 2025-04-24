namespace GithubWebApi;

/// <summary>
/// Represents a branch in a GitHub repository.
/// </summary>
[DebuggerDisplay("{Name}")]
public class Branch
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Branch"/> class.
    /// </summary>
    /// <param name="model">The model containing branch data.</param>
    internal Branch(BranchModel model)
    {
        this.Name = model.Name;
        this.Commit = model.Commit.CastModel<Commit>();
        this.Links = model.Links is not null ? new Links(model.Links) : null;
        this.Protected = model.Protected;
        //this.Protection = model.Protection is not null ? new Protection(model.Protection) : null;
        this.ProtectionUrl = model.ProtectionUrl;
    }

    /// <summary>
    /// Gets the name of the branch.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Gets the commit associated with the branch.
    /// </summary>
    public Commit? Commit { get; }

    /// <summary>
    /// Gets the links associated with the branch.
    /// </summary>
    public Links? Links { get; }

    /// <summary>
    /// Gets a value indicating whether the branch is protected.
    /// </summary>
    public bool? Protected { get; }

    /// <summary>
    /// Gets the protection settings of the branch.
    /// </summary>
    public Protection? Protection { get; }

    /// <summary>
    /// Gets the URL for the branch's protection settings.
    /// </summary>
    public string? ProtectionUrl { get; }
}
