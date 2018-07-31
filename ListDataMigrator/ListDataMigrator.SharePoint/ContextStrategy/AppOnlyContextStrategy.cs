using ListDataMigrator.Common;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Configuration;

namespace ListDataMigrator.SharePoint.ContextStrategy
{
    public class AppOnlyContextStrategy : BaseContextStrategy
    {
        private string _url;
        private string _clientId;
        private string _clientSecret;

        public override ClientContext GetContext()
        {
            var authManager = new AuthenticationManager();
            var context = authManager.GetAppOnlyAuthenticatedContext(_url, _clientId, _clientSecret);
            return context;
        }

        public override void ProcessCommandLine()
        {          
            _url = ConfigurationManager.AppSettings["url"];
            _clientId = ConfigurationManager.AppSettings["clientId"];
            _clientSecret = ConfigurationManager.AppSettings["clientSecret"];

            if (string.IsNullOrEmpty(_url) || string.IsNullOrEmpty(_clientId) || string.IsNullOrEmpty(_clientSecret))
            {
                Console.WriteLine("Please enter the following details to connect to SharePoint:");
            }

            if (string.IsNullOrEmpty(_url))
            {
                Console.WriteLine("Site URL: ");
                _url = ConsoleUtility.ReadLine();
            }

            if (string.IsNullOrEmpty(_clientId))
            {
                Console.WriteLine("Client ID: ");
                _clientId = ConsoleUtility.ReadLine();
            }

            if (string.IsNullOrEmpty(_clientSecret))
            {
                Console.WriteLine("Client Secret: ");
                _clientSecret = ConsoleUtility.ReadLine();
            }
        }
    }
}
