using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

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
            string expectedCode = File.ReadAllText(outputFile).Trim();

            Converter converter = new();
            Assert.IsTrue(converter.ConvertFromFile(inputFile));
            string code = converter.GetTestCode().Trim();

            Assert.AreEqual(expectedCode, code);
        }

        private static readonly string testDataFolder = "TestData";
        private static string getTestDataFile(string filename)
        {
            return Path.Combine(testDataFolder, filename);
        }

        private static IEnumerable<(string InputFile, string OutputFile)> GetCases()
        {
            yield return (getTestDataFile("MinimalTest.xml"), getTestDataFile("MinimalTestCode.txt"));
        }
    }
}