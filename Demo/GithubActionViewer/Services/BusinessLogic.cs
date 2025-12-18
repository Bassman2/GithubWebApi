using GithubActionViewer.ViewModel;
using GithubWebApi;
using WpfToolbox.Misc;

namespace GithubActionViewer.Services;

public sealed class BusinessLogic : IBusinessLogic, IDisposable
{
    private MainModel mainModel;

    private const string appName = "GithubActionViewer";

    public BusinessLogic()
    {
        Debug.IndentSize = 4;
        mainModel = MainModel.Load("githubactionviewer.json")!;
    }   

    public void Dispose()
    {
        // Dispose of unmanaged resources here if any
    }

    

    public ObservableCollection<ActionViewModel> Actions => actionViewModels;

    private ObservableCollection<ActionViewModel> actionViewModels = [];


    public async Task InitAsync()
    {
        await InitServersAsync();
    }

    public async Task UpdateAsync()
    {
        await UpdateRunAsync();
    }

    private async Task InitServersAsync()
    {
        ApplicationDispatcher.Invoke(() => actionViewModels.Clear());
        
        foreach (var server in mainModel.Servers)
        {
            Debug.WriteLine($"Server: {server.Name}");
            Debug.Indent();

            using var github = new Github(server.Key, appName);

            foreach (var user in server.Users)
            {
                Debug.WriteLine($"User: {user.Name} [{user.Key}]");
                Debug.Indent();
                await foreach (var repository in github.GetUserRepositoriesAsync(user.Key))
                {
                    await InitRepositoryAsync(github, repository, server, user.Branches);
                }
                Debug.Unindent();
            }

            foreach (var organization in server.Organizations)
            {
                Debug.WriteLine($"Organization: {organization.Name} [{organization.Key}]");
                Debug.Indent();
                await foreach (var repository in github.GetOrganizationRepositoriesAsync(organization.Key))
                {
                    await InitRepositoryAsync(github, repository, server, organization.Branches);
                }
                Debug.Unindent();
            }

            foreach (var repo in server.Repositories)
            {
                Debug.WriteLine($"Repositories: {repo.Name} [{repo.Key}] [{repo.Owner}]");
                Debug.Indent();
                Repository? repository = await github.GetRepositoryAsync(repo.Owner, repo.Key);
                if (repository is not null)
                {
                    await InitRepositoryAsync(github, repository, server, repo.Branches);
                }
                Debug.Unindent();
            }

            Debug.Unindent();
        }
    }
    
    private async Task InitRepositoryAsync(Github github, Repository repository, ServerModel server, List<string>? branches)
    {
        Debug.WriteLine($"Repository: {repository.Owner!.Name!} {repository.Name!}");
        Debug.Indent();

        if (branches == null)
        {
            await InitWorkflowAsync(github, repository, server, null);
        }
        else
        {
            foreach (var branch in branches)
            {
                await InitWorkflowAsync(github, repository, server, branch);
            }
        }

        Debug.Unindent();
    }

    

    private async Task InitWorkflowAsync(Github github, Repository repository, ServerModel server, string? branch)
    {
        Debug.WriteLine($"Branch: {branch}");
        Debug.Indent();

        await foreach (var workflow in github.GetRepositoryWorkflowsAsync(repository.Owner!.Login!, repository.Name!))
        {
            Debug.WriteLine($"WorkflowName: {workflow.Name}");

            var run = await github.GetWorkflowLastRunAsync(repository.Owner!.Login!, repository.Name!, workflow.Id, branch);
            if (run is not null)
            {
                var action = new ActionViewModel
                {
                    ServerKey = server.Key,
                    ServerName = server.Name,

                    RepositoryId = repository!.Id!,
                    RepositoryName = repository!.Name!,
                    RepositoryOwner = repository.Owner!.Login!,

                    WorkflowId = workflow.Id,
                    WorkflowName = workflow.Name!,
                    Run = run.Name!,
                    Branch = run.HeadBranch!,
                    Started = run.RunStartedAt,
                    RunNumber = run.RunNumber,
                    State = run.Status!,
                    Conclusion = run.Conclusion!
                };

                ApplicationDispatcher.Invoke(() => actionViewModels.Add(action));
            }
        }

        Debug.Unindent();
    }

    private async Task UpdateRunAsync()
    {

        foreach (var serverKey in actionViewModels.Select(a => a.ServerKey).Distinct())
        {
            Debug.WriteLine($"Server: {serverKey}");
            Debug.Indent();

            using var github = new Github(serverKey, appName);

            foreach (var action in actionViewModels.Where(a => a.ServerKey == serverKey))
            {
                Debug.WriteLine($"Repository: {action.RepositoryOwner}/{action.RepositoryName} - WorkflowName: {action.WorkflowName} - Branch: {action.Branch}");
                Debug.Indent();
                var run = await github.GetWorkflowLastRunAsync(action.RepositoryOwner, action.RepositoryName, action.WorkflowId, action.Branch);
                if (run is not null)
                {
                    ApplicationDispatcher.Invoke(() =>
                    {
                        action.State = run.Status!;
                        action.Conclusion = run.Conclusion!;
                        action.Started = run.RunStartedAt;
                        action.RunNumber = run.RunNumber;
                    });
                }
                Debug.Unindent();
            }
            Debug.Unindent();
        }
    }
}
