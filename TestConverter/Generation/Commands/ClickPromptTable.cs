using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    class ClickPromptTable : TableCommand
    {
        public ClickPromptTable(Command command)
            : base(command, /* additionalParms */ 0)
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
            if (!base.PreGenerate(builder))
                return;

            int row = SelectorType != ParmType.SelectionByRow ? 0 : Row;

            GenerateClickPrompt(builder, TargetControlName, row);
            GenerateSwitchFrame(builder);
        }

        private static void GenerateClickPrompt(StringBuilder builder, string controlName, int row)
        {
            GenerateClickPromptCodeWorkAround(builder, controlName, row);
            GenerateClickPromptCode(builder, controlName, row);
        }

        public static void GenerateClickPromptCodeWorkAround(StringBuilder builder, string controlName, int row)
        {
            string promptControlId = GetPromptControlId(row);
            string clickByIDCode = DriverMethodHelper.GetDriverMethodCode(MethodNames.ClickByID, StringHelper.Quote(promptControlId));
            builder.Append($"{clickByIDCode} // ");
        }

        public static void GenerateClickPromptCode(StringBuilder builder, string controlName, int row)
        {
            builder.AppendDriverMethod(MethodNames.ClickPrompt, controlName, row);
        }

        public static void GenerateSwitchFrame(StringBuilder builder)
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
