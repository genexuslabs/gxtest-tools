using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GeneXus.GXtest.Tools.TestConverter.Generation
{
    public static class StringHelper
    {

        public static string Quote(string unquoted)
        {
            return $"\"{unquoted}\"";
        }

        public static string RemoveQuotes(string quoted)
        {
            return quoted.Length >= 2 && quoted[0] == '"' && quoted[^1] == '"' ? quoted[1..^1] : quoted;
        }

        public static string[] SplitLiteralByKeys(string quotedValue)
        {
            const string pattern = "{(?<key>[A-Z]+[0-9]*)}";

            if (string.IsNullOrEmpty(quotedValue) || quotedValue == quotedEmpty)
                return new string[] { quotedEmpty };

            string value = RemoveQuotes(quotedValue);
            MatchCollection matches = Regex.Matches(value, pattern);

            List<string> fragments = new();
            int nextToUse = 0;
            foreach (Match match in matches)
            {
                // take care of eventual string before each match, eg: "some arbitrary string"
                if (match.Index > nextToUse)
                    fragments.Add(Quote(value[nextToUse..match.Index]));

                // take care of an actual match and advance the nextToUse accordingly
                fragments.Add(ConvertKeyLiteralToEnum(match.Value));
                nextToUse = match.Index + match.Length;
            }

            // take care of eventual string after last match, eg: "final words"
            if (nextToUse < value.Length)
                fragments.Add(Quote(value[nextToUse..^0]));

            return fragments.ToArray();
        }

        private static readonly string quotedEmpty = "\"\"";

        private static string ConvertKeyLiteralToEnum(string value)
        {
            // Key literals use "{SOMETHING}" pattern
            return $"Keys.{value[1..^1]}";
        }
    }
}
