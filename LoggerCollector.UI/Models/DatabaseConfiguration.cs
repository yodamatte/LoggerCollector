using DatabaseReader;

namespace LoggerCollector.UI.Models
{
    public class DatabaseConfiguration
    {
        public string Name { get; set; }
        public TableInformation TableInformation { get; set; }

        public List<TableColumns> Columns { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
