using LoggerCollector.UI.Commands;
using LoggerCollector.UI.Default;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LoggerCollector.UI.ViewModels
{
    public class MainViewModel : Observable
    {

        public ICommand NavigateCommand { get; }

        //Implement a better base class than observable
        public ObservableCollection<TabViewModel> Tabs { get; set; } = [];

        public TabViewModel? SelectedTab { get; set; } = null;

        public MainViewModel() 
        { 

            NavigateCommand = new RelayCommand<string>(Navigate, CanNavigate);
        }

        private void Navigate(string s)
        {
            Observable content = null;
            string header = s;
            if(s == "DatabaseConfiguration")
            {
                content = new DatabaseConfigurationViewModel();
            }
            else if (s == "Logger")
            {

                content = new LoggerViewModel();
            }

            if(content != null)
            {
                var tab = new TabViewModel(header, content);
                Tabs.Add(tab);
                SelectedTab = tab;
                OnPropertyChanged(nameof(SelectedTab));
            }
        }

        private bool CanNavigate(string s)
        {
            return true;
        }
    }
}
