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

using GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands;
using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation
{
    internal static class TestCodeGenerator
    {
        public static string Generate(TestCase testCase)
        {
            var builder = new StringBuilder();

            GenerateHeader(builder, testCase);
            GenerateCommands(builder, testCase);

            return builder.ToString();
        }

        private static void GenerateHeader(StringBuilder builder, TestCase testCase)
        {
            builder.AppendCommentLine($"{testCase.GeneralData.Name}");
            builder.AppendCommentLine("Converted from GXtest v3", Verbosity.Detailed);
            builder.AppendLine();

            builder.AppendCommentLine("Start webdriver");
            builder.AppendDriverMethodNoParms("Start");
            builder.AppendDriverMethodNoParms("Maximize");
            builder.AppendLine();
        }

        private static void GenerateCommands(StringBuilder builder, TestCase testCase)
        {
            foreach (Element element in TestCaseTraverser.GetElements(testCase))
            {
                builder.AppendCommentLine($"{element}", Verbosity.Diagnostic);
                GenerateElementCommands(builder, element);
            }

            GenerateEndMethod(builder);
        }

        private static void GenerateEndMethod(StringBuilder builder)
        {
            if (!GenerationOptions.General.GenerateEndMethod)
                return;

            builder.AppendDriverMethodNoParms(MethodNames.End);
        }

        private static void GenerateElementCommands(StringBuilder builder, Element element)
        {
            if (element.GetCommands().Count > 0)
            {
                foreach (Command command in element.GetCommands())
                {
                    builder.AppendCommand(command);
                }

                if (GenerationOptions.General.BlankLineAfterElement)
                    builder.AppendLine();
            }
        }
    }
}
