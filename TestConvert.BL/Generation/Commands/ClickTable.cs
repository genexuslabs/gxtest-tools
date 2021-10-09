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
