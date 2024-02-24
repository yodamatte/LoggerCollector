using LoggerCollector.UI.Default;

namespace LoggerCollector.UI.ViewModels
{
    public class TabViewModel(string header, Observable content)
    {
        public string Header { get; } = header;

        public Observable Content { get; } = content;
    }
}
