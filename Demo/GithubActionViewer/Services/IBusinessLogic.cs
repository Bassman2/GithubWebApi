namespace GithubActionViewer.Services;

public interface IBusinessLogic : IDisposable
{
    ObservableCollection<ActionViewModel> Actions { get; }

    Task InitAsync();

    Task UpdateAsync();

    
}
