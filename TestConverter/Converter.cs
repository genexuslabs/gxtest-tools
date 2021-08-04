using GeneXus.GXtest.Tools.TestConverter.v3;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

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

            using (var fileStream = File.Open(sourceFilePath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TestCase));
                var testCase = (TestCase)serializer.Deserialize(fileStream);

                testCaseInfo.Name = testCase.GeneralData.Name;
            }

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
