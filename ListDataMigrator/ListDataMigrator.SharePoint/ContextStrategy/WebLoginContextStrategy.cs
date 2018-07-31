using ListDataMigrator.Common;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Configuration;

namespace ListDataMigrator.SharePoint.ContextStrategy
{
    public class WebLoginContextStrategy : BaseContextStrategy
    {
        private string _url;

        public override ClientContext GetContext()
        {
            var authManager = new AuthenticationManager();
            var context = authManager.GetWebLoginClientContext(_url);
            return context;
        }

        public override void ProcessCommandLine()
        {
            _url = ConfigurationManager.AppSettings["url"];

            if (string.IsNullOrEmpty(_url))
            {
                Console.WriteLine("Please enter the following details to connect to SharePoint:");
                Console.WriteLine("Site URL: ");
                _url = ConsoleUtility.ReadLine();
            }
        }
    }
}
