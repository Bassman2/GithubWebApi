namespace GithubWebApi;

public class Protection
{
    internal Protection(ProtectionModel model)
    {
        this.Enabled = model.Enabled;
        this.RequiredStatusChecks = new RequiredStatusChecks(model.RequiredStatusChecks);
    }

    public bool? Enabled { get; set; }

    public RequiredStatusChecks? RequiredStatusChecks { get; set; }
}
