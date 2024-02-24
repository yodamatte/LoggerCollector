using LoggerCollector.UI.Default;

namespace LoggerCollector.UI.ViewModels
{
    public enum StatusBarStatus
    {
        Normal,
        Loading
    }

    public class StatusBarViewModel : Observable
    {
        private string statusText;
        public string StatusText
        {
            get => statusText;
            private set
            {
                statusText = value;
                OnPropertyChanged(nameof(StatusText));
            }
        }

        public void UpdateStatus(string newStatus)
        {
            StatusText = newStatus;
        }
    }
}
