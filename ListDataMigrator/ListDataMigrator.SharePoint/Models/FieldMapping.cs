namespace ListDataMigrator.SharePoint.Models
{
    public class FieldMapping
    {
        public string Type;
        public FieldDestination Destination;
    }

    public class FieldDestination
    {
        public string Type;
        public string Name;
        public string ColumnName;
    }
}
