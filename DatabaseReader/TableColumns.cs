namespace DatabaseReader
{
    public class TableColumns
    {
        public TableColumns(string columnName, bool isNullable, string dataType)
        {
            ColumnName = columnName;
            IsNullable = isNullable;
            DataType = dataType;
        }

        public string ColumnName { get; }
        public bool IsNullable { get; }
        public string DataType { get; }
    }
}
