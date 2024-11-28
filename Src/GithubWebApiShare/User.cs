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

    public string? Login { get; }

    public long? Id { get; }

    public string? NodeId { get; }

    public string? AvatarUrl { get; }



    public string? Type { get; }

    public bool? SiteAdmin { get; }

    public string? Name { get; }

    public string? Company { get; }

    public string? Location { get; }

    public string? Email { get; }
}