using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestConverterTests.Helpers
{
    public static class LineComparer
    {
        public static void AreEqual(IEnumerable<string> expectedLines, IEnumerable<string> actualLines)
        {
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

                Assert.AreEqual(expected, actual, false, $"Line {line}");
                line++;
            }
        }
    }
}
