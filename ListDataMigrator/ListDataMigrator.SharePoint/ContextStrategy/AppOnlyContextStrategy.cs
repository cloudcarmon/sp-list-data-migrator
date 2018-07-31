using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;

namespace ListDataMigrator.SharePoint.ContextStrategy
{
    public class AppOnlyContextStrategy : IContextStrategy
    {
        private string _url;
        private string _clientId;
        private string _clientSecret;

        public ClientContext GetContext()
        {
            var authManager = new AuthenticationManager();
            var context = authManager.GetAppOnlyAuthenticatedContext(_url, _clientId, _clientSecret);
            return context;
        }

        public void ProcessCommandLine()
        {
            Console.WriteLine("Please enter the following details to connect to SharePoint:");
            Console.WriteLine("Site URL: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            _url = Console.ReadLine();
            Console.ResetColor();
            Console.WriteLine("Client ID: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            _clientId = Console.ReadLine();
            Console.ResetColor();
            Console.WriteLine("Client Secret: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            _clientSecret = Console.ReadLine();
            Console.ResetColor();
        }
    }
}
