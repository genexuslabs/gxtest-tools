using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using TestConverterTests.Helpers;

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

            foreach (var (inputFile, outputFile) in GetCases())
            {
                TestFileConversion(inputFile, outputFile);
            }
        }

        private static void TestFileConversion(string inputFile, string outputFile)
        {
            Converter converter = new();
            Assert.IsTrue(converter.ConvertFromFile(inputFile));
            string code = converter.GetTestCode().Trim();

            LineComparer.AreEqual(File.ReadAllLines(outputFile), code.Split(Environment.NewLine));
        }

        private static readonly string testDataFolder = "TestData";

        private static string GetTestDataFile(string filename, string extension)
        {
            return Path.Combine(testDataFolder, filename + extension);
        }

        private static IEnumerable<(string InputFile, string OutputFile)> GetCases()
        {
            foreach (var name in testCaseNames)
                yield return (GetTestDataFile(name, ".xml"), GetTestDataFile(name, ".txt"));
        }

        private static readonly string[] testCaseNames = new string[]
            {
                "MinimalTest",
            };
    }
}