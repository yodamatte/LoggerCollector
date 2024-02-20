using System.Configuration;
using System.Data.SqlClient;

namespace DatabaseReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;

            DatabaseLoggerConfigurationHelper db = new(connectionString);
            var tables = db.GetAllDataTables();
        }
    }
}
