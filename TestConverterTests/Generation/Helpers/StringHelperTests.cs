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
            foreach ((string input, string expectedOutput) in GetCases())
            {
                Assert.AreEqual(expectedOutput, StringHelper.ProcessKeyLiterals(input));
            }
        }

        private static IEnumerable<(string input, string expectedOutput)> GetCases()
        {
            yield return ("", "\"\"");
            yield return ("\"\"", "\"\"");
            yield return ("{TAB}", "Keys.TAB");
            yield return ("\"{TAB}\"", "Keys.TAB");
            yield return ("\"algo {TAB} otro {SHIFT}\"", "\"algo \" + Keys.TAB + \" otro \" + Keys.SHIFT");
            yield return ("\"algo {TAB} otro {SHIFT} extra\"", "\"algo \" + Keys.TAB + \" otro \" + Keys.SHIFT + \" extra\"");
        }
    }
}