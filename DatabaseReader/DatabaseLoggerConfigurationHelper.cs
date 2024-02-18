using System.Data.SqlClient;

namespace DatabaseReader
{
    public class DatabaseLoggerConfigurationHelper
    {

        private string GetDatabaseNameFromConnectionString(string connectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            return builder.InitialCatalog;
        }

        public List<TableInformation> GetAllDataTables(string connectionString)
        {
            List<TableInformation> tables = new();
            // Create a SqlConnection using the connection string
            using SqlConnection connection = new(connectionString);
            string databaseName = GetDatabaseNameFromConnectionString(connectionString);
            try
            {
                // Open the connection
                connection.Open();

                // Create a SqlCommand with the SQL query
                string query = "SELECT table_schema, table_name FROM information_schema.tables";
                using SqlCommand command = new(query, connection);

                // Execute the command
                using SqlDataReader reader = command.ExecuteReader();
                // Check if there are rows returned
                if (reader.HasRows)
                {
                    Console.WriteLine("Tables in the specified database:");

                    while (reader.Read())
                    {
                        TableInformation ti = new(reader.GetString(0), reader.GetString(1));
                        tables.Add(ti);
                    }
                }
                else
                {
                    Console.WriteLine("No tables found in the specified database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return tables;
        }
    }
}
