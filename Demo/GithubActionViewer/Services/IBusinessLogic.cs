namespace GithubActionViewer.Services;

public interface IBusinessLogic : IDisposable
{
    void Update();

    List<ActionViewModel> GetActions();
}
