﻿using ListDataMigrator.Common;
using ListDataMigrator.Importer.Models;
using ListDataMigrator.SharePoint;
using ListDataMigrator.SharePoint.Models;
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

            cache.Set(CacheKeys.FILE_DIRECTORY, args.ExportedConfigDirectory, policy);
        }

        public void Run()
        {
            Console.WriteLine("Running Importer...");
        }
    }
}
