using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

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
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"// {testCaseInfo.Name}");
            builder.AppendLine();

            foreach(Element element in TestCaseTraverser.GetElements(testCase))
            {
                builder.AppendLine($"// {element}");
            }

            return builder.ToString();
        }

        public string GetTestCode()
        {
            return (testCaseInfo == null) ? string.Empty : testCaseInfo.TestCode;
        }
    }
}
