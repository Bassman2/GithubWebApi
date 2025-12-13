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
            await UpdateAsync();
        }).Wait();
    }

    private List<ActionViewModel> actionViewModels = [];    

    private async Task UpdateAsync()
    {
        actionViewModels.Clear();
        //List<ActionViewModel> actionViewModels = [];
        foreach (var server in mainModel.Servers)
        {
            Debug.WriteLine($"Server: {server.Name}");

            var github = new Github(server.Key, appName);

            foreach (var user in server.Users)
            {
                Debug.WriteLine($"User: {user.Name} [{user.Key}]");

                IAsyncEnumerable<Repository> repositories = github.GetUserRepositoriesAsync(user.Key);
                //List<Repository> repositories = repos.ToBlockingEnumerable<Repository>().ToList();
                //List<Repository> repositories = (List<Repository>)github.GetOrganizationRepositoriesAsync(organization.Key);

                await foreach (var repository in repositories)
                {

                    Debug.WriteLine($"Repository: {repository.Name}");

                    await UpdateRepositoryAsync(github, repository, server.Name);

                    //List<string> actions = ["Action"];
                    //foreach (var action in actions)
                    //{
                    //    ActionViewModel actionViewModel = new ActionViewModel
                    //    {
                    //        ServerName = server.Name,
                    //        OrganizationName = user.Name,
                    //        RepositoryName = repository.Name!,
                    //        //ActionName = action.Name,
                    //        //Status = action.Status,
                    //        //LastUpdated = action.LastUpdated
                    //    };
                    //    actionViewModels.Add(actionViewModel);
                    //}
                }
            }

            foreach (var organization in server.Organizations)
            {
                Debug.WriteLine($"Organization: {organization.Name} [{organization.Key}]");

                IAsyncEnumerable<Repository> repositories = github.GetOrganizationRepositoriesAsync(organization.Key);
                //List<Repository> repositories = repos.ToBlockingEnumerable<Repository>().ToList();
                //List<Repository> repositories = (List<Repository>)github.GetOrganizationRepositoriesAsync(organization.Key);

                await foreach (var repository in repositories)
                {

                    Debug.WriteLine($"Repository: {repository.Name}");

                    await UpdateRepositoryAsync(github, repository, server.Name);

                    //List<string> actions = ["Action"];
                    //foreach (var action in actions)
                    //{
                    //    ActionViewModel actionViewModel = new ActionViewModel
                    //    {
                    //        ServerName = server.Name,
                    //        OrganizationName = organization.Name,
                    //        RepositoryName = repository.Name!,
                    //        //ActionName = action.Name,
                    //        //Status = action.Status,
                    //        //LastUpdated = action.LastUpdated
                    //    };
                    //    actionViewModels.Add(actionViewModel);
                    //}
                }
            }

            foreach (var repo in server.Repositories)
            {
                Debug.WriteLine($"Repository: {repo.Name} [{repo.Key}] [{repo.Owner}]");

                Repository? repository = await github.GetRepositoryAsync(repo.Owner, repo.Key);

                if (repository != null)
                { 
                    await UpdateRepositoryAsync(github, repository, server.Name);
                }

                //List<string> actions = ["Action"];
                //foreach (var action in actions)
                //{
                //    ActionViewModel actionViewModel = new ActionViewModel
                //    {
                //        ServerName = server.Name,
                //        OrganizationName = repo.Owner,
                //        RepositoryName = repository!.Name!,
                //        //ActionName = action.Name,
                //        //Status = action.Status,
                //        //LastUpdated = action.LastUpdated
                //    };
                //    actionViewModels.Add(actionViewModel);
                //}
            }
           
        }
    }

    private async Task UpdateRepositoryAsync(Github github, Repository repository, string serverName)
    {
        Debug.WriteLine($"    Repository: {repository.Owner!.Name!} {repository.Name!}");

        var workflows = await github.GetRepositoryWorkflowsAsync(repository.Owner!.Login!, repository.Name!);

        foreach (var workflow in workflows ?? [])
        {

            ActionViewModel actionViewModel = new ActionViewModel
            {
                Server = serverName,
                Owner = repository.Owner!.Name!,
                Repository = repository!.Name!,
                Workflow = workflow.Name!,
                State = workflow.State!
                //ActionName = action.Name,
                //Status = action.Status,
                //LastUpdated = action.LastUpdated
            };
            actionViewModels.Add(actionViewModel);
        }
    }



    public List<ActionViewModel> GetActions()
    {        
        return actionViewModels;
    }
}
