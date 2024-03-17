using DatabaseReader.Converter;
using System.Data.SqlClient;

namespace DatabaseReader
{
    public class DatabaseLoggerConfigurationHelper
    {
        private readonly string _connectionString;

        public DatabaseLoggerConfigurationHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        private string GetDatabaseNameFromConnectionString(string connectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            return builder.InitialCatalog;
        }

        public List<TableInformation> GetAllDataTables()
        {
            List<TableInformation> tables = new();
            using SqlConnection connection = new(_connectionString);
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

        public List<TableColumns> GetAllFields(string tableName)
        {
            List<TableColumns> tables = new();

            using SqlConnection connection = new(_connectionString);
            try
            {
                connection.Open();

                // Create a SqlCommand with the SQL query
                string query = "SELECT column_name, data_type, is_nullable FROM information_schema.columns WHERE table_name = @TableName;";
                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@TableName", tableName);

                using SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        var datatype = reader.GetString(1);
                        string isNullable = reader.GetString(2);

                        TableColumns tc = new(reader.GetString(0), SQLServerToCSharpConverter.ConvertToCSharpType(datatype , isNullable));
                        tables.Add(tc);
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
