using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using GithubWebApi;

namespace GithubActionViewer.Services;

public sealed class BusinessLogic : IBusinessLogic, IDisposable
{
    private MainModel mainModel;

    private const string appName = "GithubActionViewer";

    public BusinessLogic()
    {
        mainModel = MainModel.Load("githubactionviewer.json")!;
    }   

    public void Dispose()
    {
        // Dispose of unmanaged resources here if any
    }

    public void Update()
    {
        Task.Run(async () =>
        {
            await UpdateServersAsync();
        }).Wait();
    }

    private List<ActionViewModel> actionViewModels = [];    

    private async Task UpdateServersAsync()
    {
        actionViewModels.Clear();
        foreach (var server in mainModel.Servers)
        {
            Debug.WriteLine($"Server: {server.Name}");

            var github = new Github(server.Key, appName);

            foreach (var user in server.Users)
            {
                Debug.WriteLine($"User: {user.Name} [{user.Key}]");

                await foreach (var repository in github.GetUserRepositoriesAsync(user.Key))
                {
                    await UpdateRepositoryAsync(github, repository, server.Name, user.Branches);
                }
            }

            foreach (var organization in server.Organizations)
            {
                Debug.WriteLine($"Organization: {organization.Name} [{organization.Key}]");

                await foreach (var repository in github.GetOrganizationRepositoriesAsync(organization.Key))
                {
                    await UpdateRepositoryAsync(github, repository, server.Name, organization.Branches);
                }
            }

            foreach (var repo in server.Repositories)
            {
                Debug.WriteLine($"Repository: {repo.Name} [{repo.Key}] [{repo.Owner}]");

                Repository? repository = await github.GetRepositoryAsync(repo.Owner, repo.Key);
                if (repository is not null)
                {
                    await UpdateRepositoryAsync(github, repository, server.Name, repo.Branches);
                }
            }
           
        }
    }

    

    private async Task UpdateRepositoryAsync(Github github, Repository repository, string serverName, List<string>? branches)
    {
        if (branches == null)
        {
            await UpdateWorkflow3Async(github, repository, serverName, null);
        }
        else
        {
            foreach (var branch in branches)
            {
                await UpdateWorkflow3Async(github, repository, serverName, branch);
            }
        }
    }

    private async Task UpdateWorkflowAsync(Github github, Repository repository, string serverName, string? branch)
    {
        Debug.WriteLine($"    Repository: {repository.Owner!.Name!} {repository.Name!}");

        //var workflows = await github.GetRepositoryWorkflowsAsync(repository.Owner!.Login!, repository.Name!);
               

        await foreach (var run in github.GetRepositoryWorkflowRunsAsync(repository.Owner!.Login!, repository.Name!, branch))
        {
            actionViewModels.Add(new ActionViewModel
            {
                Server = serverName,
                Owner = repository.Owner!.Login!,
                Repository = repository!.Name!,
                Workflow = run.Name!,
                Branch = run.HeadBranch!,
                Started = run.RunStartedAt,
                RunNumber = run.RunNumber,
                State = run.Status!,
                Conclusion = run.Conclusion!
            });
        }
    }

    private async Task UpdateWorkflow2Async(Github github, Repository repository, string serverName, string? branch)
    {
        Debug.WriteLine($"    Repository: {repository.Owner!.Name!} {repository.Name!}");

        await foreach (var workflow in github.GetRepositoryWorkflowsAsync(repository.Owner!.Login!, repository.Name!))
        {
            await foreach (var run in github.GetWorkflowRunsAsync(repository.Owner!.Login!, repository.Name!, workflow.Id, branch))
            {
                actionViewModels.Add(new ActionViewModel
                {
                    Server = serverName,
                    Owner = repository.Owner!.Login!,
                    Repository = repository!.Name!,
                    Workflow = run.Name!,
                    Branch = run.HeadBranch!,
                    Started = run.RunStartedAt,
                    RunNumber = run.RunNumber,
                    State = run.Status!,
                    Conclusion = run.Conclusion!
                });
            }
        }
    }

    private async Task UpdateWorkflow3Async(Github github, Repository repository, string serverName, string? branch)
    {
        Debug.WriteLine($"    Repository: {repository.Owner!.Name!} {repository.Name!}");

        await foreach (var workflow in github.GetRepositoryWorkflowsAsync(repository.Owner!.Login!, repository.Name!))
        {
            var run = await github.GetWorkflowLastRunAsync(repository.Owner!.Login!, repository.Name!, workflow.Id, branch);
            if (run is not null)
            {
                actionViewModels.Add(new ActionViewModel
                {
                    Server = serverName,
                    Owner = repository.Owner!.Login!,
                    Repository = repository!.Name!,
                    Workflow = run.Name!,
                    Branch = run.HeadBranch!,
                    Started = run.RunStartedAt,
                    RunNumber = run.RunNumber,
                    State = run.Status!,
                    Conclusion = run.Conclusion!
                });
            }
        }
    }

    public List<ActionViewModel> GetActions()
    {        
        return actionViewModels;
    }
}
