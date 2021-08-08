﻿using GeneXus.GXtest.Tools.TestConverter.Generation;
using GeneXus.GXtest.Tools.TestConverter.v3;

namespace GeneXus.GXtest.Tools.TestConverter
{
    public class Converter
    {
        private TestCaseInfo testCaseInfo = null;

        private TestCase testCase = null;

        public Converter()
        {
        }

        public bool ConvertFromFile(string sourceFilePath)
        {
            testCase = TestCase.DeserializeFromXMLfile(sourceFilePath);
            return Generate();
        }

        public bool ConvertFromString(string sourceXML)
        {
            testCase = TestCase.DeserializeFromXML(sourceXML);
            return Generate();
        }

        private bool Generate()
        {
            if (testCase == null)
                return false;

            testCaseInfo = new TestCaseInfo
            {
                Name = testCase.GeneralData.Name,
                TestCode = TestCodeGenerator.Generate(testCase)
            };

            return true;
        }

        public string GetTestCode()
        {
            return (testCaseInfo == null) ? string.Empty : testCaseInfo.TestCode;
        }
    }
}
