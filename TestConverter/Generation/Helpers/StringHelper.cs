using System.Text;
using System.Text.RegularExpressions;

namespace GeneXus.GXtest.Tools.TestConverter.Generation
{
    public static class StringHelper
    {
        public static string RemoveQuotes(string quoted)
        {
            return quoted.Length >= 2 && quoted[0] == '"' && quoted[^1] == '"' ? quoted[1..^1] : quoted;
        }

        public static string ProcessKeyLiterals(string quotedValue)
        {
            const string pattern = "{(?<key>[A-Z]+[0-9]*)}";

            if (string.IsNullOrEmpty(quotedValue) || quotedValue == quotedEmpty)
                return quotedEmpty;

            string value = RemoveQuotes(quotedValue);
            MatchCollection matches = Regex.Matches(value, pattern);

            StringBuilder builder = new();
            int nextToUse = 0;
            foreach (Match match in matches)
            {
                // take care of eventual string before each match, eg: "some arbitrary string"
                if (match.Index > nextToUse)
                    builder.ConcatLiteral(value[nextToUse..match.Index]);

                // take care of an actual match and advance the nextToUse accordingly
                builder.ConcatKey(ConvertKeyLiteralToEnum(match.Value));
                nextToUse = match.Index + match.Length;
            }

            // take care of eventual string after last match, eg: "final words"
            if (nextToUse < value.Length)
                builder.ConcatLiteral(value[nextToUse..^0]);

            return builder.ToString();
        }

        private static readonly string concatOperator = " + ";
        private static readonly string quotedEmpty = "\"\"";

        private static string ConvertKeyLiteralToEnum(string value)
        {
            // Key literals use "{SOMETHING}" pattern
            return $"Keys.{value[1..^1]}";
        }

        private static StringBuilder ConcatLiteral(this StringBuilder builder, string literalString)
        {
            return builder.ConcatTerm(literalString, true);

        }

        private static StringBuilder ConcatKey(this StringBuilder builder, string literalString)
        {
            return builder.ConcatTerm(literalString, false);

        }

        private static StringBuilder ConcatTerm(this StringBuilder builder, string value, bool quoted = false)
        {
            if (string.IsNullOrEmpty(value))
                return builder;

            if (builder.Length > 0)
                builder.Append(concatOperator);

            return quoted ? builder.AppendQuoted(value) : builder.Append(value);
        }
    }
}
