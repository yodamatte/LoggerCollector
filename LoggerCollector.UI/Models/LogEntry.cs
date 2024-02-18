using Microsoft.Extensions.Logging;

namespace LoggerCollector.UI.Models
{
    public class LogEntry
    {
        public string LogMessage { get; set; }
        public string LogSource { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}
