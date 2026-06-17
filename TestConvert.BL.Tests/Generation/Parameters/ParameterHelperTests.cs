// Copyright 2021 GeneXus S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Parameters.Tests
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