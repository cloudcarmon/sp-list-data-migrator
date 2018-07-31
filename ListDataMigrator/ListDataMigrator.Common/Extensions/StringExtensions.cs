using System.Text;

namespace ListDataMigrator.Common.Extensions
{
    public static class StringExtensions
    {
        public static string SplitOnCapitalLetters(this string inputString)
        {
            var result = new StringBuilder();

            foreach (var ch in inputString)
            {
                if (char.IsUpper(ch) && result.Length > 0)
                {
                    result.Append(' ');
                }
                result.Append(ch);
            }
            return result.ToString();
        }
    }
}
