using ListDataMigrator.SharePoint.Models;
using System.Collections.Generic;

namespace ListDataMigrator.Exporter.Models
{
    public class ExportConfig
    {
        public List<ExportList> Lists;
    }

    public class ExportList
    {
        public ExportSource Source;
        public ExportDestination Destination;
        public ExportFileOptions FileOptions;
    }

    public class ExportSource
    {
        public string Key;
        public string WebUrl;
        public string ListTitle;
        public List<string> ContentTypeNames;
    }

    public class ExportDestination
    {
        public string Type;
        public string WebUrl;
        public string ListTitle;
        public Dictionary<string, string> ContentTypeMapping;
    }

    public class ExportFileOptions
    {
        public string FieldForName;
    }

    public class ExportedItem
    {
        public Dictionary<string, object> FieldValues;
        public AssociatedFiles AssociatedFiles;
    }

    public class ExportContentType
    {
        public string Name;
        public Dictionary<string, FieldMapping> FieldMappings;
    }

    public class AssociatedFiles
    {
        public string ContentTypeFilePath;
    }
}
