using Microsoft.Extensions.Logging;
using ReadFileShare;
using System.Collections.ObjectModel;

namespace LoggerCollector.UI
{
    public class MainViewModel
    {
        public ObservableCollection<LogEntry> LogEntries { get; private set; } = new();

        private readonly Worker _worker;
        public MainViewModel()
        {
            _worker = new Worker();
            // Create an event to notify when a new line is available
            string filePath = @"C:\Users\Matte\OneDrive\Skrivbord\TestData\Test.txt";

            // Create cancellation token for the main thread
            CancellationTokenSource cancellationTokenSource = new();
            Task.Run(() => _worker.Run(filePath));
        }        

        private void HandleNewLine(object sender, string line)
        {
            LogEntry logEntry = new()
            {
                LogMessage = line,
                LogLevel = LogLevel.Information,
                LogSource = sender.ToString()
            };
            LogEntries.Add(logEntry);
        }
    }
}
