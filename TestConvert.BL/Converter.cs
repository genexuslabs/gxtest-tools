// Copyright 2021 GeneXus S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using GeneXus.GXtest.Tools.TestConvert.BL.Generation;
using GeneXus.GXtest.Tools.TestConvert.BL.v3;

namespace GeneXus.GXtest.Tools.TestConvert.BL
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
