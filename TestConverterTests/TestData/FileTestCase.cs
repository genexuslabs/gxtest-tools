using System.Collections.Generic;
using System.IO;

namespace TestConverterTests.TestData
{
    class FileTestCase
    {
        public string CaseName { get; set; }
        public string Variables { get; set; }

        public FileTestCase(string caseName = "", string variables = "")
        {
            CaseName = caseName;
            Variables = variables;
        }

        public static IEnumerable<FileTestCase> GetCases()
        {
            return new FileTestCase[]
                {
                    new FileTestCase("MinimalTest"),
                    new FileTestCase("TestSAC30166", "testmain=TestMain.Link()"),
               };
        }

        private static readonly string testDataFolder = "TestData";
        private static readonly string inputExtension = ".xml";
        private static readonly string outputExtension = ".txt";

        public string InputFile => GetDataFile(inputExtension);
        public string OutputFile => GetDataFile(outputExtension);

        private string GetDataFile(string extension)
        {
            return Path.Combine(testDataFolder, CaseName + extension);
        }
    }
}
