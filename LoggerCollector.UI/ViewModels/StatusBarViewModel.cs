using LoggerCollector.UI.Default;
using LoggerCollector.UI.Services;
using System.ComponentModel;

namespace LoggerCollector.UI.ViewModels
{
    public class StatusBarViewModel : Observable
    {
        private readonly IStatusBarService _statusBarService;

        public StatusBarViewModel(IStatusBarService statusBarService)
        {
            _statusBarService = statusBarService;
            _statusBarService.PropertyChanged += StatusBarService_PropertyChanged;
        }

        private void StatusBarService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IStatusBarService.StatusMessage))
            {
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        public string StatusMessage
        {
            get { return _statusBarService.StatusMessage; }
        }
    }
}
