using GeneXus.GXtest.Tools.TestConverter.v3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
                var parm = parmCase.Item1;
                var expected = parmCase.Item2;

                var actual = ParameterHelper.GetParameterCode(parm);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void AppendParameterTest()
        {
            foreach (var parmCase in GetParmCases())
            {
                var parm = parmCase.Item1;
                var expected = parmCase.Item2;

                StringBuilder builder = new();
                builder.AppendParameter(parm);

                var actual = builder.ToString();
                Assert.AreEqual(expected, actual);
            }
        }

        private IEnumerable<Tuple<Parameter, string>> GetParmCases()
        {
            foreach (var literalCase in GetLiteralCases())
            {
                var input = literalCase.Item1;
                var expectedOutput = literalCase.Item2;
                var parm = CreateLiteralParameter(input);
                yield return new Tuple<Parameter, string>(parm, expectedOutput);
            }
        }

        private IEnumerable<Tuple<string, string>> GetLiteralCases()
        {
            // returns <"input string", "expected output string"> tuples
            yield return new Tuple<string, string>("some value", "\"some value\"");
            yield return new Tuple<string, string>("", "\"\"");
            yield return new Tuple<string, string>(null, "\"\"");
        }

        private Parameter CreateLiteralParameter(string literal)
        {
            var literalValue = new ParameterLiteralValue();
            literalValue.Value = literal;

            var parm = new Parameter();
            parm.Type = ParameterTypes.Literal;
            parm.AddValue(literalValue);

            return parm;
        }
    }
}