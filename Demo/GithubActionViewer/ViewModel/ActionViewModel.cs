namespace GithubActionViewer.ViewModel;

public partial class ActionViewModel : ObservableObject
{
    public string Server { get; set; } = null!;
    public string Owner { get; set; } = null!;
    public string Repository { get; set; } = null!;
    public string Workflow { get; set; } = null!;
    public string Branch { get; set; } = null!;
    public DateTime? Started { get; set; }
    public int RunNumber { get; set; }
    public string State { get; set; } = null!;

    public string Conclusion { get; set; } = null!;
}
