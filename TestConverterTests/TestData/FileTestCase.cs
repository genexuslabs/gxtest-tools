using System.Collections.Generic;
using System.IO;

namespace TestConverterTests.TestData
{
    class FileTestCase
    {
        public string CaseName { get; set; }
        public string Variables { get; set; }

        public bool BlankLineAfterElement { get; set; } = true;

        public bool GenerateEndMethod { get; set; } = true;

        public FileTestCase(string caseName = "", string variables = "")
        {
            CaseName = caseName;
            Variables = variables;
        }

        public static IEnumerable<FileTestCase> GetCases()
        {
            return new FileTestCase[]
                {
                    new FileTestCase("MinimalTest")
                    {
                        BlankLineAfterElement = false,
                        GenerateEndMethod = false
                    },
                    new FileTestCase("TestSAC30166", "testmain=TestMain.Link()"),
                    new FileTestCase("TestSAC29742", "testmain=TestMain.Link()"),
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
