using ListDataMigrator.Common.Extensions;
using ListDataMigrator.SharePoint.Extensions;
using ListDataMigrator.SharePoint.Models;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Linq.Expressions;
using System.Runtime.Caching;

namespace ListDataMigrator.SharePoint.Cache
{
    public static class SharePointCache
    {
        public static void PrepareCache()
        {
            ObjectCache cache = MemoryCache.Default;

            var context = cache.Get<ClientContext>(SharePointCacheKeys.SP_CONTEXT);
            CacheItemPolicy policy = new CacheItemPolicy();
            cache.Set("/", context.Web, policy);

            cache.RegisterTypeDelegate<Web>((Func<ClientContext, string, Web>)((ctx, url) =>
            {
                var newWeb = ctx.Site.OpenWeb(url);
                ctx.Load(newWeb, w => w.ServerRelativeUrl);
                ctx.ExecuteQuery();
                return newWeb;
            }));

            cache.RegisterTypeDelegate<ContentType>((Func<ClientContext, string, ContentType>)((ctx, name) =>
            {
                return ctx.Site.RootWeb.GetContentTypeByName(name);
            }));

            cache.RegisterTypeDelegate<TermGroup>((Func<ClientContext, Guid, Guid, TermGroup>)((ctx, termStoreId, termSetId) =>
            {
                var session = TaxonomySession.GetTaxonomySession(ctx);
                var store = session.TermStores.GetById(termStoreId);
                var termSet = store.GetTermSet(termSetId);
                var termGroup = termSet.Group;
                ctx.Load(termGroup);
                ctx.ExecuteQuery();
                return termGroup;
            }));

            cache.RegisterTypeDelegate<User>((Func<ClientContext, UserInformation, User>)((ctx, userInformation) =>
            {
                try
                {
                    var user = ctx.Web.EnsureUser(userInformation.EMail);
                    ctx.Load(user);
                    ctx.ExecuteQuery();
                    return user;
                }
                catch (Exception)
                {
                    Console.WriteLine($"User does not exist: {userInformation.EMail}");
                    return null;
                }
            }));

            cache.RegisterTypeDelegate<List>((Func<Web, string, ListTemplateType, Expression<Func<List, object>>[], List>)((web, name, templateType, expressions) =>
            {
                return web.EnsureList(name, templateType, expressions);
            }));
        }
    }
}
