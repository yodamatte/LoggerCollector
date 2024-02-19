using System.Data.SqlClient;

namespace DatabaseReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=TEST.eSIGN;Integrated Security=True;";

            DatabaseLoggerConfigurationHelper db = new();
            var tables = db.GetAllDataTables(connectionString);
        }
    }
}
