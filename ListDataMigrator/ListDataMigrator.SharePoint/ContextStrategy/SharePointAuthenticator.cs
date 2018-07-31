using ListDataMigrator.Common.Extensions;
using System;
using System.Runtime.Caching;

namespace ListDataMigrator.SharePoint.ContextStrategy
{
    public static class SharePointAuthenticator
    {
        public static void Connect()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("List Data Migrator");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("How would you like to connect to SharePoint?");

            Console.ForegroundColor = ConsoleColor.Blue;
            var contextStrategyValues = Enum.GetValues(typeof(ContextStrategyType));
            foreach (int value in contextStrategyValues)
            {
                var strategyString = ((ContextStrategyType)value).ToString();
                var split = strategyString.SplitOnCapitalLetters();
                Console.WriteLine($"{value}. {split}");
            }
            
            Console.WriteLine($"{contextStrategyValues.Length + 1}. Exit");
            Console.ResetColor();
            Console.WriteLine("Please enter a number from the above options:");

            Console.ForegroundColor = ConsoleColor.Blue;
            var result = Console.ReadLine();
            Console.ResetColor();

            var parsed = Enum.IsDefined(typeof(ContextStrategyType), Convert.ToInt32(result));
            if (parsed)
            {
                var contextStrategy = (ContextStrategyType)Enum.Parse(typeof(ContextStrategyType), result);
                var strategy = ContextStrategyFactory.GetContextStrategy(contextStrategy);
                strategy.ProcessCommandLine();
                Console.WriteLine("Connecting to SharePoint...");
                var context = strategy.GetContext();
                context.Load(context.Web);
                context.ExecuteQuery();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Connected");
                Console.ResetColor();

                ObjectCache cache = MemoryCache.Default;
                CacheItemPolicy policy = new CacheItemPolicy();
                cache.Set(SharePointCacheKeys.SP_CONTEXT, context, policy);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
