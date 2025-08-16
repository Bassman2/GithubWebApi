namespace GithubWebApi;

public class Meta
{
   internal Meta(MetaModel model)
   {
      this.InstalledVersion = model.InstalledVersion;
   }

    public string? InstalledVersion { get; }
}
