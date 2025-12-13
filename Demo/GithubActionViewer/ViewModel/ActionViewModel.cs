namespace GithubActionViewer.ViewModel;

public partial class ActionViewModel : ObservableObject
{
    public string Server { get; set; } = null!;
    public string Owner { get; set; } = null!;
    public string Repository { get; set; } = null!;
    public string Workflow { get; set; } = null!;
    public string State { get; set; } = null!;
}
