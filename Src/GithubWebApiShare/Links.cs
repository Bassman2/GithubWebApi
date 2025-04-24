namespace GithubWebApi;

/// <summary>
/// Represents a collection of links related to a GitHub resource.
/// </summary>
public class Links
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Links"/> class.
    /// </summary>
    /// <param name="model">The model containing link data.</param>
    internal Links(LinksModel model)
    {
        this.Self = model.Self;
        this.Html = model.Html;
    }

    /// <summary>
    /// Gets the URL pointing to the API endpoint of the resource.
    /// </summary>
    public string? Self { get; }

    /// <summary>
    /// Gets the URL pointing to the HTML representation of the resource.
    /// </summary>
    public string? Html { get; }
}
