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
                    new FileTestCase("Test - FF", "testmain=TestMain.Link()"),
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
