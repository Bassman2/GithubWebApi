namespace GithubWebApi;

/// <summary>
/// Represents a user in a GitHub repository.
/// </summary>
public class User
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="model">The model containing user data.</param>
    internal User(UserModel model)
    {
        this.Login = model.Login;
        this.Id = model.Id;
        this.NodeId = model.NodeId;
        this.AvatarUrl = model.AvatarUrl;
        this.Type = model.Type;
        this.SiteAdmin = model.SiteAdmin;
        this.Name = model.Name;
        this.Company = model.Company;
        this.Location = model.Location;
        this.Email = model.Email;
    }

    /// <summary>
    /// Gets the login username of the user.
    /// </summary>
    public string? Login { get; internal init; }

    /// <summary>
    /// Gets the unique identifier of the user.
    /// </summary>
    public long? Id { get; internal init; }

    /// <summary>
    /// Gets the Node ID of the user.
    /// </summary>
    public string? NodeId { get; internal init; }

    /// <summary>
    /// Gets the URL of the user's avatar image.
    /// </summary>
    public string? AvatarUrl { get; internal init; }

    /// <summary>
    /// Gets the type of the user (e.g., "User" or "Organization").
    /// </summary>
    public string? Type { get; internal init; }

    /// <summary>
    /// Gets a value indicating whether the user is a site administrator.
    /// </summary>
    public bool? SiteAdmin { get; internal init; }

    /// <summary>
    /// Gets the name of the user.
    /// </summary>
    public string? Name { get; internal init; }

    /// <summary>
    /// Gets the company associated with the user.
    /// </summary>
    public string? Company { get; internal init; }

    /// <summary>
    /// Gets the location of the user.
    /// </summary>
    public string? Location { get; internal init; }

    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    public string? Email { get; internal init; }
}
