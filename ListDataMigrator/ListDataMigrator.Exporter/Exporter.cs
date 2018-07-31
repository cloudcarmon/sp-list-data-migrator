using ListDataMigrator.Common;
using ListDataMigrator.Exporter.Models;
using ListDataMigrator.SharePoint;
using ListDataMigrator.SharePoint.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace ListDataMigrator.Exporter
{
    public class Exporter
    {
        public Exporter(CommandLineArguments args)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();

            var contentTypeMapping = JsonUtility.FromFile<List<ContentTypeMapping>>(args.ContentTypeMappingPath);

            cache.Set(SharePointCacheKeys.CONTENT_TYPE_MAPPING, contentTypeMapping, policy);
            cache.Set(CacheKeys.FILE_DIRECTORY, args.OutputPath, policy);
            // Json load export 
        }

        public void Run()
        {
            Console.WriteLine("Running Exporter...");
        }
    }
}
