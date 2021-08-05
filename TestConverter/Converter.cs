using GeneXus.GXtest.Tools.TestConverter.v3;

namespace GeneXus.GXtest.Tools.TestConverter
{
    class Converter
    {
        private string sourceFilePath { get; set; }

        private TestCaseInfo testCaseInfo = null;

        private TestCase testCase = null;

        public Converter(string sourceFile)
        {
            this.sourceFilePath = sourceFile;
        }

        public bool Convert()
        {
            testCaseInfo = new TestCaseInfo();

            testCase = TestCase.DeserializeFromXML(sourceFilePath);
            if (testCase == null)
                return false;

            testCaseInfo.Name = testCase.GeneralData.Name;
            testCaseInfo.TestCode = CreateTestCode();
            return true;
        }

        private string CreateTestCode()
        {
            return $"// {testCaseInfo.Name}";
        }

        public string GetTestCode()
        {
            return (testCaseInfo == null) ? string.Empty : testCaseInfo.TestCode;
        }
    }
}
