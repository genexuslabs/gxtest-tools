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

using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands
{
    class ClickPromptTable : TableCommand
    {
        private const int AdditionalParms = 0;

        public ClickPromptTable(Command command)
            : base(command, AdditionalParms)
        {
            Debug.Assert(command.Name == CommandNames.ClickPromptTable);
        }

        public override void Generate(StringBuilder builder)
        {
            // ClickPromptTable(
            //      [0] ignore    - ParameterBooleanValue[false],
            //      [1] grid      - ParameterControlValue[a4126e3c-555b-4570-86c5-da96ec7da727],
            //      [2] byRow     - /* SelectionByRow */ null,
            //      [3] row       - ParameterLiteralValue[2],
            //      [4] CountryId - ParameterControlValue[401bbfb2-14de-4e7c-b79b-c2c0aaf12d0f]
            // )
            if (!PreGenerate(builder))
                return;

            GenerateClickPrompt(builder, TargetControlName, Row);
            GenerateSwitchFrame(builder);
            GenerationState.State.OnPrompt = true;
        }

        protected override bool PreGenerate(StringBuilder builder)
        {
            if (!base.PreGenerate(builder))
                return false;

            if (!UsesRowSelector)
            {
                builder.AppendLine("code not yet implemented");
                return false;
            }

            return true;
        }

        private static void GenerateClickPrompt(StringBuilder builder, string controlName, int row)
        {
            GenerateClickPromptCodeWorkAround(builder, controlName, row);
            GenerateClickPromptCode(builder, controlName, row);
        }

        private static void GenerateClickPromptCodeWorkAround(StringBuilder builder, string _/*controlName*/, int row)
        {
            string promptControlId = GetPromptControlId(row);
            string clickByIDCode = DriverHelper.GetDriverMethodCode(MethodNames.ClickByID, StringHelper.Quote(promptControlId));
            builder.Append($"{clickByIDCode} // ");
        }

        private static void GenerateClickPromptCode(StringBuilder builder, string controlName, int row)
        {
            builder.AppendDriverMethod(MethodNames.ClickPrompt, controlName, row);
        }

        private static void GenerateSwitchFrame(StringBuilder builder)
        {
            _ = builder.AppendLine("&driver.SwitchFrame(\"index=0\") // should not be needed");
        }

        private static string GetPromptControlId(int row, int gridId = 1)
        {
            // e.g.: "PROMPT_1_0002"
            return $"PROMPT_{gridId}_{row:D4}";
        }
    }
}
