using CamlexNET;
using Microsoft.SharePoint.Client;
using System.Collections.Generic;
using System.Linq;

namespace ListDataMigrator.SharePoint.Extensions
{
    public static class ListExtensions
    {
        public static List<ListItem> GetAllItemsPaged(this List list, List<string> viewFields = null, int rowLimit = 500)
        {
            var allItems = new List<ListItem>();
            var caml = Camlex.Query();

            if (viewFields != null && viewFields.Any())
            {
                caml.ViewFields(viewFields.ToArray());
            }
            
            caml.Take(rowLimit).Scope(ViewScope.RecursiveAll);

            var camlQuery = caml.ToCamlQuery();
            do
            {
                var queriedItems = list.GetItems(camlQuery);
                list.Context.Load(queriedItems);
                list.Context.ExecuteQuery();
                camlQuery.ListItemCollectionPosition = queriedItems.ListItemCollectionPosition;
                allItems.AddRange(queriedItems.ToList());
            } while (camlQuery.ListItemCollectionPosition != null);

            return allItems;
        }
    }
}
