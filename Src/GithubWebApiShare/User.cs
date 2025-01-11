namespace GithubWebApi;

public class User
{
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

    public string? Login { get; internal init; }

    public long? Id { get; internal init; }

    public string? NodeId { get; internal init; }

    public string? AvatarUrl { get; internal init; }



    public string? Type { get; internal init; }

    public bool? SiteAdmin { get; internal init; }

    public string? Name { get; internal init; }

    public string? Company { get; internal init; }

    public string? Location { get; internal init; }

    public string? Email { get; internal init; }
}