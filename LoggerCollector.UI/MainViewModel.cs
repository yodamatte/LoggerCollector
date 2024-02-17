using Microsoft.Extensions.Logging;
using ReadFileShare;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LoggerCollector.UI
{
    public class MainViewModel
    {
        private readonly string _filePath = @"C:\Users\Matte\OneDrive\Skrivbord\TestData\Test.txt";
        public ObservableCollection<LogEntry> LogEntries { get; private set; } = new();
        public ICommand RunCommandAsync { get; }
        public ICommand CancelCommandAsync { get; }

        private readonly CancellationTokenSource _cts;

        private readonly Worker _worker;
        public MainViewModel()
        {
            _worker = new Worker();

            RunCommandAsync = new RelayCommandAsync<string>(ExecuteRunCommand, CanRun);
            CancelCommandAsync = new RelayCommandAsync<string>(ExecuteCancelCommand, CanCancel);

            _cts = new();
        }

        private async Task<bool> CanCancel(string parameter)
        {
            return _worker.Running;
        }

        private async Task ExecuteCancelCommand(string parameter)
        {
            _cts.Cancel();
        }

        private async Task<bool> CanRun(string parameter)
        {
            return !_worker.Running;
        }

        private async Task ExecuteRunCommand(string parameter)
        {
            await _worker.Run(_filePath, _cts, HandleNewLine);
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
