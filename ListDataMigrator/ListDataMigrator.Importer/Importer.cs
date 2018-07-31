using ListDataMigrator.Common;
using ListDataMigrator.Common.Extensions;
using ListDataMigrator.Importer.Models;
using ListDataMigrator.SharePoint;
using ListDataMigrator.SharePoint.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace ListDataMigrator.Importer
{
    public class Importer
    {
        public Importer(CommandLineArguments args)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();

            var userInformationList = JsonUtility.FromFile<Dictionary<string, UserInformation>>(args.UserInformationListPath);
            var contentTypeMapping = JsonUtility.FromFile<List<ContentTypeMapping>>(args.ContentTypeMappingPath);

            cache.Set(SharePointCacheKeys.USER_INFORMATION_LIST, userInformationList, policy);
            cache.Set(SharePointCacheKeys.CONTENT_TYPE_MAPPING, contentTypeMapping, policy);

            cache.Set(CacheKeys.FILE_DIRECTORY, contentTypeMapping, policy);
        }

        public void Run()
        {
            ObjectCache cache = MemoryCache.Default;
            var web = cache.Get<Web>("/");
            Console.WriteLine(web.Title);
            Console.WriteLine("Running Importer...");
        }
    }
}
