using LoggerCollector.UI.Commands;
using LoggerCollector.UI.Default;
using LoggerCollector.UI.Services;
using System.Collections.ObjectModel;
using System.Reflection.PortableExecutable;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace LoggerCollector.UI.ViewModels;

public class MainViewModel : Observable
{

    public ICommand NavigateCommand { get; }

    public ICommand CloseTabCommand { get; }

    public IStatusBarService StatusBarService { get; }
    public ILoggerNavigationService NavigationService { get; }

    //Implement a better base class than observable
    public ObservableCollection<TabViewModel> Tabs { get; set; } = [];

    public TabViewModel? SelectedTab { get; set; } = null;

    public MainViewModel(
        IStatusBarService statusBarService,
        ILoggerNavigationService navigationService) 
    {
        StatusBarService = statusBarService;
        NavigationService = navigationService;
        StatusBarService.StatusMessage = "Init";
        NavigateCommand = new RelayCommand<string>(Navigate, CanNavigate);
        CloseTabCommand = new RelayCommand<TabViewModel>(CloseTab);

        NavigationService.Navigated += NavigationService_Navigated;
    }

    private void NavigationService_Navigated(object? sender, LoggerNavigationEventArgs e)
    {
        var tab = new TabViewModel(e.Header, e.Content);
        Application.Current.Dispatcher.Invoke(() =>
        {
            Tabs.Add(tab);
            SelectedTab = tab;
            OnPropertyChanged(nameof(SelectedTab));
        });
    }

    private void CloseTab(TabViewModel tab)
    {
        Tabs.Remove(tab);
    }

    private async void Navigate(string s)
    {
        if(Tabs.Any(x => x.Header == s))
        {
            return;
        }

        await NavigationService.Navigate(s);
    }

    private bool CanNavigate(string s)
    {
        return true;
    }
}
