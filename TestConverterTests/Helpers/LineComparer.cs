using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestConverterTests.Helpers
{
    public static class LineComparer
    {
        public static void AreEqual(IEnumerable<string> expectedLines, IEnumerable<string> actualLines, string caseName = "")
        {
            string caseInfo = string.IsNullOrEmpty(caseName) ? "" : $"Case '{caseName}'";

            IEnumerator<string> expectedEnum = expectedLines.GetEnumerator();
            IEnumerator<string> actualEnum = actualLines.GetEnumerator();

            bool gotExpected = true;
            bool gotActual = true;
            int line = 1;
            while (
                // kind of tricky...
                //   '|' (rather than '||') so it tries to advance in both enums
                //   '&&' so that, for each enum, it advances only if it didn't already reached its end 
                (gotExpected = gotExpected && expectedEnum.MoveNext()) |
                (gotActual = gotActual && actualEnum.MoveNext())
                )
            {
                string expected = gotExpected ? expectedEnum.Current : string.Empty;
                string actual = gotActual ? actualEnum.Current : string.Empty;

                Assert.AreEqual(expected, actual, false, $"{caseInfo}, Line {line}, Position {GetFirstDiff(expected, actual)}");
                line++;
            }
        }

        private static int GetFirstDiff(string a, string b)
        {
            // check up to minimal common length
            int minLength = Math.Min(a.Length, b.Length);
            for (int i = 0; i < minLength; i++)
            {
                if (a[i] != b[i])
                    return i;
            }

            // if we couldn't find differences up to common length
            // and they are both same lenght, then they are actually equal
            if (a.Length == b.Length)
                return -1;

            // if they aren't same length, the first different char is
            // the one following the last of the shortest string
            return (a.Length < b.Length) ? a.Length : b.Length;
        }
    }
}
