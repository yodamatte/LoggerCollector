
namespace DatabaseReader.Converter;
public class SQLServerToCSharpConverter
{
    public static Type ConvertToCSharpType(string sqlServerType, string isNullable)
    {
        bool nullable = isNullable.ToLower() == "yes";
        return ConvertToCSharpType(sqlServerType, nullable);
    }

    private static Type ConvertToCSharpType(string sqlServerType, bool isNullable)
    {
        Type type = ConvertToCSharpType(sqlServerType);

        if (isNullable && type != typeof(string))
        {
            return typeof(Nullable<>).MakeGenericType(type);
        }

        return type;
    }

    private static Type ConvertToCSharpType(string sqlServerType)
    {
        switch (sqlServerType.ToLower())
        {
            case "bigint":
                return typeof(long);
            case "int":
                return typeof(int);
            case "smallint":
                return typeof(short);
            case "tinyint":
                return typeof(byte);
            case "bit":
                return typeof(bool);
            case "decimal":
            case "numeric":
                return typeof(decimal);
            case "money":
            case "smallmoney":
                return typeof(decimal); // or typeof(double) if precision is not critical
            case "float":
                return typeof(double);
            case "real":
                return typeof(float);
            case "date":
            case "datetime":
            case "datetime2":
                return typeof(DateTime);
            case "time":
                return typeof(TimeSpan);
            case "datetimeoffset":
                return typeof(DateTimeOffset);
            case "char":
            case "nchar":
            case "varchar":
            case "nvarchar":
            case "text":
            case "ntext":
                return typeof(string);
            case "binary":
            case "varbinary":
            case "image":
                return typeof(byte[]);
            case "uniqueidentifier":
                return typeof(Guid);
            case "xml":
                // You might handle XML differently based on your XML processing library
                return typeof(System.Xml.XmlDocument); // or typeof(System.Xml.Linq.XDocument) if using LINQ to XML
            default:
                throw new ArgumentException($"Unsupported SQL Server data type: {sqlServerType}");
        }
    }
}
