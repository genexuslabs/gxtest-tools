using System;
using System.IO;

namespace GeneXus.GXtest.Tools.TestConverter
{
    class Converter
    {
        private string sourceFilePath { get; set; }

        private TestCaseInfo testCaseInfo = null;

        public string TestCode { get; set; }  = string.Empty;

        public Converter(string sourceFile)
        {
            this.sourceFilePath = sourceFile;
        }

        public bool LoadFromXML()
        {
            testCaseInfo = new TestCaseInfo();

            // verify file exists
            if (!File.Exists(sourceFilePath))
            {
                Console.Error.WriteLine($"Source XML file does not exist '{sourceFilePath}'");
                return false;
            }

            testCaseInfo.Name = "NameOfTheTest";
            return true;
        }

        public bool CreateTestCode()
        {
            if (testCaseInfo == null)
            {
                Console.Error.WriteLine("Attempt to create code before loading test info");
                return false;
            }

            TestCode = $"// {testCaseInfo.Name}";
            return true;
        }
    }
}
