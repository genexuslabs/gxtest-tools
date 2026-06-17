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

using GeneXus.GXtest.Tools.TestConvert.BL;
using GeneXus.GXtest.Tools.TestConvert.BL.Generation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TestConverterTests.Helpers;
using TestConverterTests.TestData;

namespace GeneXus.GXtest.Tools.TestConverter.Tests
{
    [TestClass()]
    public class ConverterTests
    {
        [TestMethod()]
        public void ConvertFromFileTest()
        {
            string file = "does not exist";
            Converter converter = new();
            Assert.IsFalse(converter.ConvertFromFile(file));

            file = "<Invalid path>";
            Assert.IsFalse(converter.ConvertFromFile(file));

            foreach (var testCase in FileTestCase.GetCases())
            {
                TestFileConversion(testCase);
            }
        }

        private static void TestFileConversion(FileTestCase testCase)
        {
            Converter converter = new();
            GenerationOptions.General.SetVariables(testCase.Variables);
            GenerationOptions.General.BlankLineAfterElement = testCase.BlankLineAfterElement;
            GenerationOptions.General.GenerateEndMethod = testCase.GenerateEndMethod;

            Assert.IsTrue(converter.ConvertFromFile(testCase.InputFile));
            string code = converter.GetTestCode().Trim();

            LineComparer.AreEqual(File.ReadAllLines(testCase.OutputFile), code.Split(Environment.NewLine), testCase.CaseName);
        }
    }
}