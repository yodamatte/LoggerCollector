using LoggerCollector.UI.Commands;
using LoggerCollector.UI.Default;
using LoggerCollector.UI.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace LoggerCollector.UI.ViewModels;

public class MainViewModel : Observable
{

    public ICommand NavigateCommand { get; }

    public ICommand CloseTabCommand { get; }

    public IStatusBarService StatusBarService { get; }

    //Implement a better base class than observable
    public ObservableCollection<TabViewModel> Tabs { get; set; } = [];

    public TabViewModel? SelectedTab { get; set; } = null;

    public MainViewModel(IStatusBarService statusBarService) 
    {
        StatusBarService = statusBarService;
        StatusBarService.StatusMessage = "Init";
        NavigateCommand = new RelayCommand<string>(Navigate, CanNavigate);
        CloseTabCommand = new RelayCommand<TabViewModel>(CloseTab);
    }

    private void CloseTab(TabViewModel tab)
    {
        Tabs.Remove(tab);
    }

    private async void Navigate(string s)
    {
        Observable content = null;
        string header = s;

        if(Tabs.Any(x=> x.Header == header))
        {
            return;
        }
        if(s == "DatabaseConfiguration")
        {
            var config = new DatabaseConfigurationViewModel();
            var task = Task.Run(config.Load);

            StatusBarService.StatusMessage = "Loading";
            await task.ConfigureAwait(false);
            StatusBarService.StatusMessage = "Done";
            content = config;
        }
        else if (s == "Logger")
        {

            content = new LoggerViewModel();
        }

        if(content != null)
        {
            var tab = new TabViewModel(header, content);
            Application.Current.Dispatcher.Invoke(() =>
            {
                Tabs.Add(tab);
                SelectedTab = tab;
                OnPropertyChanged(nameof(SelectedTab));
            });
        }
    }

    private bool CanNavigate(string s)
    {
        return true;
    }
}
