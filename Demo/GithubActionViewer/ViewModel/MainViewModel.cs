namespace GithubActionViewer.ViewModel;

public sealed partial class MainViewModel : AppViewModel
{
    private readonly IBusinessLogic businessLogic;

    public MainViewModel(IBusinessLogic businessLogic)
    {  
        this.businessLogic = businessLogic;
    }

    protected override void OnStartup()
    {
        this.businessLogic.Update();
        this.Actions = this.businessLogic.GetActions();
    }

    [ObservableProperty]
    private List<ActionViewModel> actions = [];

}
