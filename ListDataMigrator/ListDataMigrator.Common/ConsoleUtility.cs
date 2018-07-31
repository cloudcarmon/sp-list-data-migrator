using System;
using System.Security;

namespace ListDataMigrator.Common
{
    public static class ConsoleUtility
    {
        public static SecureString ReadPasswordAsSecureString()
        {
            var password = new SecureString();
            var key = Console.ReadKey(true);

            while (key.Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.RemoveAt(password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password.AppendChar(key.KeyChar);
                    Console.Write("*");
                }
                key = Console.ReadKey(true);
            }

            Console.WriteLine();
            return password;
        }
    }
}
