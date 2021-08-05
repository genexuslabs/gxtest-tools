using GeneXus.GXtest.Tools.TestConverter.v3;
using System;

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

            TestCase testCase = TestCase.DeserializeFromXML(sourceFilePath);
            if (testCase == null)
                return false;

            testCaseInfo.Name = testCase.GeneralData.Name;

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
