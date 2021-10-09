using GeneXus.GXtest.Tools.TestConvert.BL.Generation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Tests
{
    [TestClass()]
    public class StringHelperTests
    {
        [TestMethod()]
        public void CharAtTest()
        {
            foreach ((string input, int signedIndex, char expectedOutput) in GetCharAtCases())
            {
                // check against explicit expected result
                Assert.AreEqual(input.CharAt(signedIndex), expectedOutput);

                // check compatibility with Index operator
                Assert.AreEqual(input.CharAt(signedIndex), signedIndex >= 0 ? input[signedIndex] : input[^-signedIndex]);
            }
        }

        private static IEnumerable<(string input, int signedIndex, char expectedOutput)> GetCharAtCases()
        {
            const string someText = "some text";

            yield return (someText, 0, 's');
            yield return (someText, 1, 'o');
            yield return (someText, 2, 'm');
            yield return (someText, 3, 'e');
            // ...
            yield return (someText, -3, 'e');
            yield return (someText, -2, 'x');
            yield return (someText, -1, 't');
        }

        [TestMethod()]
        public void RangeTest()
        {
            foreach ((string input, int signedStart, int signedEnd, string expectedOutput) in GetRangeCases())
            {
                string rangeMethodResult = input.Range(signedStart, signedEnd);

                // check against explicit expected result
                Assert.AreEqual(rangeMethodResult, expectedOutput);

                // check compatibility with Range expressions
                Index start = new(Math.Abs(signedStart), signedStart < 0);
                Index end = new(Math.Abs(signedEnd), signedEnd < 0);
                string rangeExpressionResult = input[start..end];
                Assert.AreEqual(rangeMethodResult, rangeExpressionResult);
            }
        }

        private static IEnumerable<(string input, int signedStart, int signedEnd, string expectedOutput)> GetRangeCases()
        {
            const string someText = "012345678";

            yield return (someText, 0, 0, "");
            yield return (someText, 0, 1, "0");
            yield return (someText, 2, 4, "23");
            yield return (someText, 0, 8, "01234567");
            yield return (someText, 2, 8, "234567");
            yield return (someText, 2, -1, "234567");
            yield return (someText, 2, -2, "23456");
            yield return (someText, -4, -1, "567");
            yield return (someText, -1, -1, "");
        }

        [TestMethod()]
        public void ProcessKeyLiteralsTest()
        {
            foreach ((string input, string[] expectedOutput) in GetProcessKeyLiteralsCases())
            {
                CollectionAssert.AreEqual(expectedOutput, StringHelper.SplitLiteralByKeys(input));
            }
        }

        private static IEnumerable<(string input, string[] expectedOutput)> GetProcessKeyLiteralsCases()
        {
            yield return ("", new string[] { "\"\"" });
            yield return ("\"\"", new string[] { "\"\"" });
            yield return ("{TAB}", new string[] { "Keys.TAB" });
            yield return ("\"{TAB}\"", new string[] { "Keys.TAB" });
            yield return ("\"algo {TAB} otro {SHIFT}\"", new string[] { "\"algo \"", "Keys.TAB", "\" otro \"", "Keys.SHIFT" });
            yield return ("\"algo {TAB} otro {SHIFT} extra\"", new string[] { "\"algo \"", "Keys.TAB", "\" otro \"", "Keys.SHIFT", "\" extra\"" });
        }

        [TestMethod()]
        public void RemoveQuotesTest()
        {
            foreach ((string input, string expectedOutput) in GetRemoveQuotesCases())
            {
                Assert.AreEqual(StringHelper.RemoveQuotes(input), expectedOutput);
            }
        }

        private static IEnumerable<(string input, string expectedOutput)> GetRemoveQuotesCases()
        {
            yield return ("", "");
            yield return ("\"", "\"");
            yield return ("\"\"", "");
            yield return ("\"\"\"\"", "\"\"");
            yield return ("\"s\"", "s");
            yield return ("\"something\"", "something");
            yield return ("\"something", "\"something");
            yield return ("something\"", "something\"");
        }
    }
}