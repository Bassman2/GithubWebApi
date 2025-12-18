namespace GithubActionViewer.ViewModel;

public sealed partial class MainViewModel : AppViewModel
{
    private readonly IBusinessLogic businessLogic;

    public MainViewModel(IBusinessLogic businessLogic)
    {  
        this.businessLogic = businessLogic;
        this.Actions = businessLogic.Actions;
    }

    protected override void OnStartup()
    {
        Task.Run(async () =>
        {
            IsScanning = true;
            await this.businessLogic.InitAsync();
            IsScanning = false;
        });
    }

    private bool OnCanUpdate => !IsScanning;

    [RelayCommand(CanExecute = nameof(OnCanUpdate))]
    private void OnUpdate()
    {
        Task.Run(async () =>
        {
            IsScanning = true;
            await this.businessLogic.UpdateAsync();
            IsScanning = false;
        });
    }

    [ObservableProperty]
    private ObservableCollection<ActionViewModel> actions = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(OnCanUpdate))]
    private bool isScanning;

}
