using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation
{
    public static class StringHelper
    {
        private static int ResolveSignedIndex(this string text, int signedIndex)
        {
            // negative index are taken as relative to sequence Length
            return signedIndex >= 0 ? signedIndex : text.Length + signedIndex;
        }

        public static char CharAt(this string text, int signedIndex)
        {
            return text[text.ResolveSignedIndex(signedIndex)];
        }

        public static string Range(this string text, int start, int end)
        {
            // both start and end indexes taken as Signed
            int actualStart = text.ResolveSignedIndex(start);
            int actualEnd = text.ResolveSignedIndex(end);

            // Ranges are exclusive, meaning the end isn't included in the range 
            int rangeLength = actualEnd - actualStart;
            return text.Substring(actualStart, rangeLength);
        }

        public static string Quote(string unquoted)
        {
            return $"\"{unquoted}\"";
        }

        public static string RemoveQuotes(string quoted)
        {
            //  return quoted.Length >= 2 && quoted[0] == '"' && quoted[^1] == '"' ? quoted[1..^1] : quoted;
            return quoted.Length >= 2 && quoted[0] == '"' && quoted.CharAt(-1) == '"' ? quoted.Range(1, -1) : quoted;
        }

        public static string[] SplitLiteralByKeys(string quotedValue)
        {
            const string pattern = "{(?<key>[A-Z]+[0-9]*)}";

            if (string.IsNullOrEmpty(quotedValue) || quotedValue == quotedEmpty)
                return new string[] { quotedEmpty };

            string value = RemoveQuotes(quotedValue);
            MatchCollection matches = Regex.Matches(value, pattern);

            var fragments = new List<string>();
            int nextToUse = 0;
            foreach (Match match in matches)
            {
                // take care of eventual string before each match, eg: "some arbitrary string"
                if (match.Index > nextToUse)
                    fragments.Add(Quote(value.Range(nextToUse, match.Index)));

                // take care of an actual match and advance the nextToUse accordingly
                fragments.Add(ConvertKeyLiteralToEnum(match.Value));
                nextToUse = match.Index + match.Length;
            }

            // take care of eventual string after last match, eg: "final words"
            if (nextToUse < value.Length)
                fragments.Add(Quote(value.Substring(nextToUse)));

            return fragments.ToArray();
        }

        private static readonly string quotedEmpty = "\"\"";

        private static string ConvertKeyLiteralToEnum(string value)
        {
            // Key literals use "{SOMETHING}" pattern
            return $"Keys.{value.Range(1, -1)}";
        }
    }
}
