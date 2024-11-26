namespace GithubWebApi;

public class User
{
    internal User(UserModel model)
    {
        this.Login = model.Login;
        this.Id = model.Id;
        this.NodeId = model.NodeId;
        this.AvatarUrl = model.AvatarUrl;
    }

    public string? Login { get; }

    public int? Id { get; }

    public string? NodeId { get; }

    public string? AvatarUrl { get; }
}