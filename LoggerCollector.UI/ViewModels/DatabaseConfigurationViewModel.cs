using DatabaseReader;
using LoggerCollector.UI.Commands;
using LoggerCollector.UI.Default;
using LoggerCollector.UI.Models;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Input;

namespace LoggerCollector.UI.ViewModels;

public class DatabaseConfigurationViewModel : Observable
{
    public ObservableCollection<TableInformation> Tables { get; private set; }

    public ObservableCollection<TableColumns> Columns { get; private set; } = new();

    public string DatabaseConfigurationName { get; set; }

    public ObservableCollection<DatabaseConfiguration> Configurations { get; set; } = new();


    //TODO Cleanup this mess
    private DatabaseConfiguration _selectedConfig;
    public DatabaseConfiguration SelectedConfig
    {
        get { return _selectedConfig; }
        set
        {
            if (_selectedConfig != value)
            {
                _selectedConfig = value;
                OnPropertyChanged(nameof(SelectedConfig));
                DatabaseConfigurationName = _selectedConfig.Name;
                OnPropertyChanged(nameof(DatabaseConfigurationName));
                SelectedTable = _selectedConfig.TableInformation;
                OnPropertyChanged(nameof(SelectedTable));
            }
        }
    }

    private TableInformation _selectedTable;

    public TableInformation SelectedTable
    {
        get { return _selectedTable; }
        set
        {
            if (_selectedTable != value)
            {
                _selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));
                LoadColumns(_selectedTable.TableName);
            }
        }
    }

    private readonly DatabaseLoggerConfigurationHelper _databaseLoggerConfigurationHelper;

    private void LoadColumns(string tableName)
    {
        Columns.Clear();
        foreach (var column in _databaseLoggerConfigurationHelper.GetAllFields(tableName))
        {
            Columns.Add(column);
        }
    }

    public ICommand SaveCommand { get; }

    public DatabaseConfigurationViewModel() 
    {
        SaveCommand = new RelayCommand<string>(Save, CanSave);
        var connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;

        _databaseLoggerConfigurationHelper = new(connectionString);
        var tables = _databaseLoggerConfigurationHelper.GetAllDataTables();
        Tables = new(tables);
    }

    private bool CanSave(string obj)
    {
        return SelectedTable != null;
    }

    private void Save(string obj)
    {
        DatabaseConfiguration config = new()
        {
            TableInformation = _selectedTable,
            Columns = new(Columns),
            Name = DatabaseConfigurationName
        };


        Configurations.Add(config);
        SelectedConfig = config;
    }
}
