namespace GithubActionViewer.ViewModel;

public partial class ActionViewModel : ObservableObject
{
    public string ServerKey { get; set; } = null!;
    public string ServerName { get; set; } = null!;


    public long RepositoryId { get; set; }
    public string RepositoryName { get; set; } = null!;
    public string RepositoryOwner { get; set; } = null!;

    public long WorkflowId { get; set; }
    public string WorkflowName { get; set; } = null!;
    public string Run { get; set; } = null!;
    public string Branch { get; set; } = null!;



    [ObservableProperty]
    private DateTime? started;

    [ObservableProperty]
    private int runNumber;

    [ObservableProperty]
    private string state = null!;

    [ObservableProperty]
    private string conclusion = null!;
}
