using CommunityToolkit.Mvvm.DependencyInjection;

using GithubActionViewer.View;
using System.Configuration;
using System.Data;
using System.Windows;

namespace GithubActionViewer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void OnStartup(object sender, StartupEventArgs e)
    {
        Trace.TraceInformation("Startup {0} {1}", DateTime.Now.ToLocalTime().ToShortTimeString(), DateTime.Now.ToLocalTime().ToShortDateString());
               
        AppDomain.CurrentDomain.UnhandledException += (s, a) =>
        {
            Exception ex = (Exception)a.ExceptionObject;
            Trace.TraceError(ex.ToString());
            MessageBox.Show(ex.ToString(), "Unhandled Error !!!");
        };

        Ioc.Default.ConfigureServices
        (
            new ServiceCollection()
                .AddSingleton<IBusinessLogic, BusinessLogic>()
                .AddScoped<MainViewModel>()
                .BuildServiceProvider()
        );

        new GithubActionViewer.View.MainView().Show();

    }

    protected override void OnExit(ExitEventArgs e)
    {
        Ioc.Default.GetRequiredService<IBusinessLogic>().Dispose();
        base.OnExit(e);
    }
        
}
