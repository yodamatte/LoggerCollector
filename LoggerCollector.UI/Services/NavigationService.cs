using LoggerCollector.UI.Default;
using LoggerCollector.UI.ViewModels;

namespace LoggerCollector.UI.Services
{
    public interface ILoggerNavigationService
    {
        event EventHandler<LoggerNavigationEventArgs> Navigated;
        Task Navigate(string header);
    }

    public class LoggerNavigationEventArgs(Observable content, string header) : EventArgs
    {
        public Observable Content { get; } = content;

        public string Header { get; } = header;
    }

    public class LoggerNavigationService : ILoggerNavigationService
    {
        public event EventHandler<LoggerNavigationEventArgs> Navigated;
        public LoggerNavigationService(IStatusBarService statusBarService)
        {
            StatusBarService = statusBarService;
        }

        public IStatusBarService StatusBarService { get; }

        public async Task Navigate(string header)
        {
            StatusBarService.StatusMessage = $"Loading view {header}";
            Observable content = null;

            if (header == "DatabaseConfiguration")
            {
                var config = new DatabaseConfigurationViewModel();
                var task = Task.Run(config.Load);

                StatusBarService.StatusMessage = "Loading";
                await task.ConfigureAwait(false);
                StatusBarService.StatusMessage = "Done";
                content = config;
            }
            else if (header == "Logger")
            {
                content = new LoggerViewModel();
            }

            OnNavigated(content, header);
        }

        protected virtual void OnNavigated(Observable content, string header)
        {
            StatusBarService.StatusMessage = $"Loaded view {header}";
            Navigated?.Invoke(this, new LoggerNavigationEventArgs(content, header));
        }
    }
}
