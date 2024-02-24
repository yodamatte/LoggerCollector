using LoggerCollector.UI.Default;

namespace LoggerCollector.UI.ViewModels
{
    public class TabViewModel
    {
        public TabViewModel(string header, Observable content)
        {
            Header = header;
            Content = content;
        }

        public string Header { get; }

        public Observable Content { get; }

    }
}
