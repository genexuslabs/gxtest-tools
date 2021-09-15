using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Tests
{
    [TestClass()]
    public class StringHelperTests
    {
        [TestMethod()]
        public void ProcessKeyLiteralsTest()
        {
            foreach ((string input, string[] expectedOutput) in GetCases())
            {
                CollectionAssert.AreEqual(expectedOutput, StringHelper.SplitLiteralByKeys(input));
            }
        }

        private static IEnumerable<(string input, string[] expectedOutput)> GetCases()
        {
            yield return ("", new string[] { "\"\"" });
            yield return ("\"\"", new string[] { "\"\"" });
            yield return ("{TAB}", new string[] { "Keys.TAB" });
            yield return ("\"{TAB}\"", new string[] { "Keys.TAB" });
            yield return ("\"algo {TAB} otro {SHIFT}\"", new string[] { "\"algo \"", "Keys.TAB", "\" otro \"", "Keys.SHIFT" });
            yield return ("\"algo {TAB} otro {SHIFT} extra\"", new string[] { "\"algo \"", "Keys.TAB", "\" otro \"", "Keys.SHIFT", "\" extra\"" });
        }
    }
}