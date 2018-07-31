using ListDataMigrator.Common.Extensions;
using Microsoft.SharePoint.Client;
using System;
using System.Runtime.Caching;

namespace ListDataMigrator.SharePoint.ContextStrategy
{
    public class SharePointAuthenticator
    {
        public void Connect()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("List Data Migrator");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("How would you like to connect to SharePoint?");

            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (int value in Enum.GetValues(typeof(ContextStrategyType)))
            {
                var strategyString = ((ContextStrategyType)value).ToString();
                var split = strategyString.SplitOnCapitalLetters();
                Console.WriteLine($"{value}. {split}");
            }
            
            Console.WriteLine("4. Exit");
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
                var context = strategy.GetContext();
                context.Load(context.Web, w => w.Title);
                context.ExecuteQuery();

                Console.WriteLine(context.Web.Title);

                ObjectCache cache = MemoryCache.Default;
                CacheItemPolicy policy = new CacheItemPolicy();
                cache.Set("context", context, policy);

                Console.WriteLine("Saved to cache");

                var c = cache.Get<ClientContext>("context");

                Console.WriteLine(c.Web.Title);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
