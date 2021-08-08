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
            foreach (var (Parm, Expected, IgnoreCase) in GetParmCases())
            {
                var actual = ParameterHelper.GetParameterCode(Parm);
                Assert.AreEqual(Expected, actual, IgnoreCase);
            }
        }

        [TestMethod()]
        public void AppendParameterTest()
        {
            foreach (var (Parm, Expected, IgnoreCase) in GetParmCases())
            {
                StringBuilder builder = new();
                builder.AppendParameter(Parm);

                string actual = builder.ToString();
                Assert.AreEqual(Expected, actual, IgnoreCase);
            }
        }

        private static IEnumerable<(Parameter Parm, string Expected, bool IgnoreCase)> GetParmCases()
        {
            foreach (var (InputValue, Expectedoutput) in GetLiteralCases())
            {
                var parm = Parameter.CreateLiteralParameter(InputValue);
                yield return (parm, Expectedoutput, true);
            }

            foreach (var (InputValue, Expectedoutput) in GetBooleanCases())
            {
                var parm = Parameter.CreateBooleanParameter(InputValue);
                yield return (parm, Expectedoutput, true);
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