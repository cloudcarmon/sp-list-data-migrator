using Microsoft.SharePoint.Client;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ListDataMigrator.SharePoint.Extensions
{
    public static class WebExtensions
    {
        public static ContentType GetContentTypeByName(this Web web, string contentTypeName)
        {
            if (string.IsNullOrEmpty(contentTypeName))
            {
                return null;
            }

            var results = web.Context.LoadQuery(web.ContentTypes.Where(item => item.Name == contentTypeName));
            web.Context.ExecuteQuery();
            return results.FirstOrDefault();
        }

        public static List EnsureList(this Web web, string listName, ListTemplateType templateType = ListTemplateType.GenericList)
        {
            return web.EnsureList(listName, templateType);
        }

        public static List EnsureList(this Web web, string listName, ListTemplateType templateType, params Expression<Func<List, object>>[] expressions)
        {
            List list;
            try
            {
                list = web.Lists.GetByTitle(listName);
                web.Context.Load(list, expressions);
                web.Context.ExecuteQuery();
            }
            catch (Exception)
            {
                var createInfo = new ListCreationInformation
                {
                    Title = listName,
                    TemplateType = templateType == ListTemplateType.NoListTemplate ? (int)ListTemplateType.GenericList : (int)templateType
                };

                list = web.Lists.Add(createInfo);
                web.Context.Load(list, expressions);
                web.Context.ExecuteQuery();
            }

            if (!list.ContentTypesEnabled)
            {
                list.ContentTypesEnabled = true;
                list.Update();
                web.Context.Load(list, expressions);
                web.Context.ExecuteQuery();
            }

            return list;
        }
    }
}
