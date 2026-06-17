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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Tests
{
    [TestClass()]
    public class GenerationOptionsTests
    {
        [TestMethod()]
        public void SetVariablesTest()
        {
            var options = GenerationOptions.General;

            foreach ((string input, GenerationOptions.Variable[] expectedVars) in GetCases())
            {
                options.SetVariables(input);
                Assert.AreEqual(expectedVars.Length, options.Variables.Count);
                foreach (var variable in expectedVars)
                {
                    // variable is found
                    Assert.IsTrue(options.Variables.ContainsKey(variable.Name));

                    // variable is found even with different case
                    Assert.IsTrue(options.Variables.ContainsKey(variable.Name.ToUpper()));
                    Assert.IsTrue(options.Variables.ContainsKey(variable.Name.ToLower()));

                    // variables have equal content
                    Assert.AreEqual(variable, options.Variables[variable.Name]);
                }
            }
        }

        private static IEnumerable<(string input, GenerationOptions.Variable[] expectedVars)> GetCases()
        {
            // trivial case
            yield return (
                "varname=42",
                new GenerationOptions.Variable[]
                {
                    new ("varname", "42")
                }
            );

            // trivial multiple
            yield return (
                "varname=42;another=other",
                new GenerationOptions.Variable[]
                {
                    new ("varname", "42"),
                    new ("another", "other"),
                }
            );

            // space & semicolon injection
            yield return (
                " varname =42 ;another= other; yetAnother = something;;whatIf=not; ",
                new GenerationOptions.Variable[]
                {
                    new ("varname", "42"),
                    new ("another", "other"),
                    new ("yetAnother", "something"),
                    new ("whatIf", "not"),
                }
            );

            // keep last when duplicate name
            yield return (
                "varname=41;varname=42",
                new GenerationOptions.Variable[]
                {
                    new ("varname", "42"),
                }
            );

            // name is not case sensitive when storing
            yield return (
                "varname=41;VARname=42",
                new GenerationOptions.Variable[]
                {
                    new ("VARname", "42"),
                }
            );

            // value may be empty
            yield return (
                "varname=",
                new GenerationOptions.Variable[]
                {
                    new ("varname", "")
                }
            );

            // assignment operator is not needed when value is empty
            yield return (
                "varname",
                new GenerationOptions.Variable[]
                {
                    new ("varname", "")
                }
            );

            // & before the name is accepted and stripped
            yield return (
                "&varname=34",
                new GenerationOptions.Variable[]
                {
                    new ("varname", "34")
                }
            );
        }
    }
}