using System.Collections.Generic;

namespace ListDataMigrator.SharePoint.Models
{
    public class ContentTypeMapping
    {
        public string Name;
        public string Parent;
        public List<string> Fields;
        public Dictionary<string, FieldMapping> FieldMappings;
    }
}
