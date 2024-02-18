using System.Data.SqlClient;

namespace DatabaseReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Logger;Integrated Security=True;";

            DatabaseLoggerConfigurationHelper db = new DatabaseLoggerConfigurationHelper();
            var tables = db.GetAllDataTables(connectionString);
        }
    }
}
