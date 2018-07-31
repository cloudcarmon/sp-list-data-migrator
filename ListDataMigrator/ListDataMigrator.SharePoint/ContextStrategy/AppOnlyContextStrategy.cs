using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;

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
            Console.WriteLine("Please enter the following details to connect to SharePoint:");
            Console.WriteLine("Site URL: ");
            _url = ReadLine();
            Console.WriteLine("Client ID: ");
            _clientId = ReadLine();
            Console.WriteLine("Client Secret: ");
            _clientSecret = ReadLine();
        }
    }
}
