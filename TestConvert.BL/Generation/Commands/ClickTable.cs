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
using System;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands
{
    class ClickTable : TableCommand
    {
        private const int AdditionalParms = 0;

        public ClickTable(Command command)
            : base(command, AdditionalParms)
        {
            Debug.Assert(command.Name == CommandNames.ClickTable);
        }

        public override void Generate(StringBuilder builder)
        {
            if (!PreGenerate(builder))
                return;

            string gridName = GridControlName;
            string controlName = TargetControlName;

            if (IsDeleteRow(gridName, controlName))
            {
                GenerateDeleteRow(builder, gridName, Row);
                return;
            }

            builder.AppendDriverMethod(MethodNames.Click, controlName, RowExpression);
            if (GenerationState.State.OnPrompt)
            {
                GeneratePromptExit(builder);
                GenerationState.State.OnPrompt = false;
            }
        }

        private static void GeneratePromptExit(StringBuilder builder)
        {
            _ = builder.AppendLine("&driver.SwitchFrame(\"relative=parent\") // should not be needed");
        }

        private static bool IsDeleteRow(string gridName, string controlName)
        {
            return string.Compare(StringHelper.RemoveQuotes(controlName), $"delete{StringHelper.RemoveQuotes(gridName)}", true) == 0;
        }

        private static void GenerateDeleteRow(StringBuilder builder, string gridName, int row)
        {
            GenerateDeleteRowCodeWorkAround(builder, gridName, row);
            GenerateDeleteRowCode(builder, gridName, row);
        }

        private static void GenerateDeleteRowCodeWorkAround(StringBuilder builder, string gridName, int row)
        {
            string deleteRowControlId = GetDeleteRowControlId(gridName, row);
            string clickByIDCode = DriverHelper.GetDriverMethodCode(MethodNames.ClickByID, deleteRowControlId);
            builder.Append($"{clickByIDCode} // ");
        }

        private static void GenerateDeleteRowCode(StringBuilder builder, string gridName, int row)
        {
            builder.AppendDriverMethod(MethodNames.DeleteRow, gridName, row);
        }

        private static string GetDeleteRowControlId(string quotedGridName, int row)
        {
            string gridName = StringHelper.RemoveQuotes(quotedGridName);
            string titleCaseGridName = gridName.Length <= 0 ? string.Empty : Char.ToUpper(gridName[0]) + gridName.Substring(1).ToLower();

            return $"\"delete{StringHelper.RemoveQuotes(titleCaseGridName)}_{row:D4}\"";
        }
    }
}
