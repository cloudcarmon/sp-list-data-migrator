using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;

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
            Console.WriteLine("Please enter the following details to connect to SharePoint:");
            Console.WriteLine("Site URL: ");
            _url = ReadLine();
        }
    }
}
