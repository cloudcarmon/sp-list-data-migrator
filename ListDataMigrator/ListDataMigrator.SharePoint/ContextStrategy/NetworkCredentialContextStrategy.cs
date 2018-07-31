using ListDataMigrator.Common;
using Microsoft.SharePoint.Client;
using System;
using System.Security;

namespace ListDataMigrator.SharePoint.ContextStrategy
{
    public class NetworkCredentialContextStrategy : IContextStrategy
    {
        private string _url;
        private string _email;
        private SecureString _password;

        public ClientContext GetContext()
        {
            var context = new ClientContext(_url)
            {
                Credentials = new SharePointOnlineCredentials(_email, _password)
            };
            return context;
        }

        public void ProcessCommandLine()
        {
            Console.WriteLine("Please enter the following details to connect to SharePoint");
            Console.WriteLine("Site URL: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            _url = Console.ReadLine();
            Console.ResetColor();
            Console.WriteLine("Email: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            _email = Console.ReadLine();
            Console.ResetColor();
            Console.WriteLine("Password: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            _password = ConsoleUtility.ReadPasswordAsSecureString();
            Console.ResetColor();
        }
    }
}
