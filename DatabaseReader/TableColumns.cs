namespace DatabaseReader
{
    public class TableColumns
    {
        public TableColumns(string columnName, Type dataType)
        {
            ColumnName = columnName;
            DataType = dataType;
        }

        public string ColumnName { get; }
        public Type DataType { get; }
    }
}
