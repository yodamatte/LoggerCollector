using LoggerCollector.UI.Default;

namespace LoggerCollector.UI.ViewModels
{
    public class MainViewModel : Observable
    {
        public LoggerViewModel LoggerViewModel { get; private set; }
        public DatabaseConfigurationViewModel DatabaseConfigurationViewModel { get; private set; }

        public MainViewModel() 
        { 
            LoggerViewModel = new LoggerViewModel();
            DatabaseConfigurationViewModel = new DatabaseConfigurationViewModel();
        }
    }
}
