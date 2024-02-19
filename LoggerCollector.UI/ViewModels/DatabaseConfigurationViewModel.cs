using DatabaseReader;
using LoggerCollector.UI.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LoggerCollector.UI.ViewModels
{
    public class DatabaseConfigurationViewModel
    {
        public ObservableCollection<TableInformation> Tables { get; private set; }

        public TableInformation SelectedTable { get; set; }

        public ICommand SaveCommand { get; }

        public DatabaseConfigurationViewModel() 
        {
            SaveCommand = new RelayCommand<string>(Save, CanSave);
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=TEST.eSIGN;Integrated Security=True;";

            DatabaseLoggerConfigurationHelper db = new();
            var tables = db.GetAllDataTables(connectionString);
            Tables = new(tables);   
        }

        private bool CanSave(string obj)
        {
            return SelectedTable != null;
        }

        private void Save(string obj)
        {
        }
    }
}
