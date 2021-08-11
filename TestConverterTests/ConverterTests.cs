using GeneXus.GXtest.Tools.TestConverter.Generation;
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

            Assert.IsTrue(converter.ConvertFromFile(testCase.InputFile));
            string code = converter.GetTestCode().Trim();

            LineComparer.AreEqual(File.ReadAllLines(testCase.OutputFile), code.Split(Environment.NewLine), testCase.CaseName);
        }
    }
}