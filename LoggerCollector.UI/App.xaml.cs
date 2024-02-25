using Autofac;
using LoggerCollector.UI.Services;
using LoggerCollector.UI.ViewModels;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace LoggerCollector;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<MainWindow>().AsSelf();
        builder.RegisterType<MainViewModel>().AsSelf();
        builder.RegisterType<StatusBarService>().As<IStatusBarService>();

        var container = builder.Build();

        using (var scope = container.BeginLifetimeScope())
        {
            var window = scope.Resolve<MainWindow>();
            window.Show();
        }
    }
}
