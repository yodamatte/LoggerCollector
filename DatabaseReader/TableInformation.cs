namespace DatabaseReader
{
    public class TableInformation
    {
        public TableInformation(string schema, string tableName) 
        {
            Schema = schema;
            TableName = tableName;
        }
        public string Schema { get; private set; }
        public string TableName { get; private set; }

        public override string ToString()
        {
            return $"{Schema}.{TableName}";
        }
    }
}
