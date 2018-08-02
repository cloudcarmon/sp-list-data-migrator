using ListDataMigrator.Common.Extensions;
using ListDataMigrator.SharePoint.Extensions;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ListDataMigrator.SharePoint.Services
{
    public class ListExporter
    {
        private Web _web;
        private string _listTitle;
        private List<string> _viewFields;

        public ListExporter(Web web, string listTitle, List<string> viewFields)
        {
            _web = web;
            _listTitle = listTitle;
            _viewFields = viewFields;
        }

        public void ExportItems(string path, string fieldNameKeyBy = null)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var list = _web.Lists.GetByTitle(_listTitle);
            var allItems = list.GetAllItemsPaged(_viewFields);

            var fieldValues = allItems.Select(item => item.FieldValues);

            var file = Path.Combine(path, _listTitle.GetSafeFilename());
            var serialized = JsonConvert.SerializeObject(fieldValues, Formatting.Indented, jsonSettings);
            System.IO.File.WriteAllText(file, serialized);
        }
    }
}
