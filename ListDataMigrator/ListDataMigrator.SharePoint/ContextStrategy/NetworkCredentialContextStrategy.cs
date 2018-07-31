using ListDataMigrator.Common;
using Microsoft.SharePoint.Client;
using System;
using System.Security;

namespace ListDataMigrator.SharePoint.ContextStrategy
{
    public class NetworkCredentialContextStrategy : BaseContextStrategy
    {
        private string _url;
        private string _email;
        private SecureString _password;

        public override ClientContext GetContext()
        {
            var context = new ClientContext(_url)
            {
                Credentials = new SharePointOnlineCredentials(_email, _password)
            };
            return context;
        }

        public override void ProcessCommandLine()
        {
            Console.WriteLine("Please enter the following details to connect to SharePoint");
            Console.WriteLine("Site URL: ");
            _url = ReadLine();
            Console.WriteLine("Email: ");
            _email = ReadLine();
            Console.WriteLine("Password: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            _password = ConsoleUtility.ReadPasswordAsSecureString();
            Console.ResetColor();
        }
    }
}
