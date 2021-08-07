using GeneXus.GXtest.Tools.TestConverter.v3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters.Tests
{
    [TestClass()]
    public class ParameterHelperTests
    {
        [TestMethod()]
        public void GetParameterCodeTest()
        {
            foreach (var parmCase in GetParmCases())
            {
                var actual = ParameterHelper.GetParameterCode(parmCase.Parm);
                Assert.AreEqual(parmCase.Expected, actual, parmCase.IgnoreCase);
            }
        }

        [TestMethod()]
        public void AppendParameterTest()
        {
            foreach (var parmCase in GetParmCases())
            {
                StringBuilder builder = new();
                builder.AppendParameter(parmCase.Parm);

                string actual = builder.ToString();
                Assert.AreEqual(parmCase.Expected, actual, parmCase.IgnoreCase);
            }
        }

        private static IEnumerable<(Parameter Parm, string Expected, bool IgnoreCase)> GetParmCases()
        {
            foreach (var literalCase in GetLiteralCases())
            {
                var parm = Parameter.CreateLiteralParameter(literalCase.InputValue);
                yield return (parm, literalCase.Expectedoutput, true);
            }

            foreach (var booleanCase in GetBooleanCases())
            {
                var parm = Parameter.CreateBooleanParameter(booleanCase.InputValue);
                yield return (parm, booleanCase.Expectedoutput, true);
            }
        }

        private static IEnumerable<(string InputValue, string Expectedoutput)> GetLiteralCases()
        {
            yield return ("some value", "\"some value\"");
            yield return ("", "\"\"");
            yield return (null, "\"\"");
        }

        private static IEnumerable<(string InputValue, string Expectedoutput)> GetBooleanCases()
        {
            yield return ("false", "false");
            yield return ("whatever", "false");
            yield return ("", "false");
            yield return ("true", "true");
        }
    }
}