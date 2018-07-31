﻿using CommandLine;
using ListDataMigrator.Importer.Models;
using ListDataMigrator.SharePoint.Cache;
using ListDataMigrator.SharePoint.ContextStrategy;
using System;

namespace ListDataMigrator.Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<CommandLineArguments>(args).WithParsed(Run);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            finally
            {
                Console.Write("Press Enter to close window ...");
                Console.ResetColor();
                Console.Read();
            }
        }

        private static void Run(CommandLineArguments args)
        {
            SharePointAuthenticator.Connect();
            SharePointCache.PrepareCache();
            var importer = new Importer(args);
            importer.Run();
        }
    }
}
