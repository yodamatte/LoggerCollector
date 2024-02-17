using Microsoft.Extensions.Logging;

namespace LoggerCollector.UI
{
    public class LogEntry
    {
        public string LogMessage { get; set; }
        public string LogSource { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}
