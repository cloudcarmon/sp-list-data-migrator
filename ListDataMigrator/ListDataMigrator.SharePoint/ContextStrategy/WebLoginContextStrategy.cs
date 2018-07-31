using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;

namespace ListDataMigrator.SharePoint.ContextStrategy
{
    public class WebLoginContextStrategy : IContextStrategy
    {
        private string _url;

        public ClientContext GetContext()
        {
            var authManager = new AuthenticationManager();
            var context = authManager.GetWebLoginClientContext(_url);
            return context;
        }

        public void ProcessCommandLine()
        {
            Console.WriteLine("Please enter the following details to connect to SharePoint:");
            Console.WriteLine("Site URL: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            _url = Console.ReadLine();
            Console.ResetColor();
        }
    }
}
