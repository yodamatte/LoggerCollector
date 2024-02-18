using LoggerCollector.UI.Commands;
using LoggerCollector.UI.Default;
using LoggerCollector.UI.Models;
using Microsoft.Extensions.Logging;
using ReadFileShare;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace LoggerCollector.UI
{
    public class MainViewModel : Observable
    {
        private readonly string _filePath = @"C:\Users\Matte\OneDrive\Skrivbord\TestData\Test.txt";
        public ObservableCollection<LogEntry> LogEntries { get; private set; } = new();
        public ICommand RunCommandAsync { get; }
        public ICommand CancelCommandAsync { get; }
        public int LogCount { get; private set; }

        private CancellationTokenSource _cts;

        private readonly Worker _worker;
        public MainViewModel()
        {
            _worker = new Worker();

            RunCommandAsync = new RelayCommandAsync<string>(ExecuteRunCommand, CanRun);
            CancelCommandAsync = new RelayCommand<string>(ExecuteCancelCommand, CanCancel);
            LogEntries.CollectionChanged += LogEntries_CollectionChanged;
        }

        private void LogEntries_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            LogCount = LogEntries.Count;
            OnPropertyChanged(nameof(LogCount));
        }

        private bool CanCancel(string parameter)
        {
            Debug.WriteLine($"Can Cancel: {_worker.Running}");
            return _worker.Running;
        }

        private void ExecuteCancelCommand(string parameter)
        {
            Debug.WriteLine("Cancelling");
            _cts.Cancel();
        }

        private bool CanRun(string parameter)
        {
            Debug.WriteLine($"Can Run: {!_worker.Running}");
            return !_worker.Running;
        }

        private async Task ExecuteRunCommand(string parameter)
        {
            Debug.WriteLine("Executing Run");
            _cts = new();
            await _worker.Run(_filePath, _cts, HandleNewLine);
        }

        private void HandleNewLine(object? sender, string line)
        {
            Debug.WriteLine($"Handling new line: {line}");
            Application.Current.Dispatcher.Invoke(() =>
            {
                LogEntry logEntry = new()
                {
                    LogMessage = line,
                    LogLevel = LogLevel.Information,
                    LogSource = sender?.ToString()
                };
                LogEntries.Add(logEntry);
            });
        }
    }
}
