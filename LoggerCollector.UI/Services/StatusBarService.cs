using LoggerCollector.UI.Default;
using System.ComponentModel;

namespace LoggerCollector.UI.Services;

public interface IStatusBarService : INotifyPropertyChanged
{
    string StatusMessage { get; set; }
}

public class StatusBarService : Observable, IStatusBarService
{
    private string _statusMessage;

    public string StatusMessage
    {
        get { return _statusMessage; }
        set
        {
            _statusMessage = value;
            OnPropertyChanged(nameof(StatusMessage));
        }
    }
}
