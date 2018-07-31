using ListDataMigrator.Common;
using Microsoft.SharePoint.Client;
using System;
using System.Configuration;
using System.Security;

namespace ListDataMigrator.SharePoint.ContextStrategy
{
    public class NetworkCredentialContextStrategy : BaseContextStrategy
    {
        private string _url;
        private string _username;
        private SecureString _password;

        public override ClientContext GetContext()
        {
            var context = new ClientContext(_url)
            {
                Credentials = new SharePointOnlineCredentials(_username, _password)
            };
            return context;
        }

        public override void ProcessCommandLine()
        {
            Console.WriteLine("Please enter the following details to connect to SharePoint");
            _url = ConfigurationManager.AppSettings["url"];
            _username = ConfigurationManager.AppSettings["username"];

            if (string.IsNullOrEmpty(_url))
            {
                Console.WriteLine("Site URL: ");
                _url = ConsoleUtility.ReadBlueLine();
            }
            if (string.IsNullOrEmpty(_username))
            {
                Console.WriteLine("Email: ");
                _username = ConsoleUtility.ReadBlueLine();
            }
            
            Console.WriteLine($"Password ({_username}): ");
            Console.ForegroundColor = ConsoleColor.Blue;
            _password = ConsoleUtility.ReadPasswordAsSecureString();
            Console.ResetColor();
        }
    }
}
