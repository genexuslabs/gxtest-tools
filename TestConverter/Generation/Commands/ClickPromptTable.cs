using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    class ClickPromptTable : CommandGenerator
    {
        public ClickPromptTable(Command command)
            : base(command)
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

            builder.AppendCommentLine("ClickPromptTable command generation", Verbosity.Diagnostic);
            builder.AppendCommentLine($"Ignoring first parm {Command.Parameters[0]}", Verbosity.Diagnostic);
            builder.AppendCommentLine($"Ignoring second parm {Command.Parameters[1]}", Verbosity.Diagnostic);

            ParmType selectionType = Command.Parameters[2].Type;
            if (selectionType != ParmType.SelectionByRow)
            {
                builder.AppendLine("code not yet implemented");
                return;
            }

            if (Command.Parameters.Count < 5)
            {
                builder.AppendLine("not enough parameters");
                return;
            }

            string rowId = StringHelper.RemoveQuotes(ParameterHelper.GetParameterCode(Command.Parameters[3]));
            string controlName = ParameterHelper.GetParameterCode(Command.Parameters[4]);
            GenerateClickPrompt(builder, controlName, rowId);
            GenerateSwitchFrame(builder);
        }

        private static void GenerateClickPrompt(StringBuilder builder, string controlName, string rowId)
        {
            GenerateClickPromptCodeWorkAround(builder, controlName, rowId);
            GenerateClickPromptCode(builder, controlName, rowId);
        }

        public static void GenerateClickPromptCodeWorkAround(StringBuilder builder, string controlName, string rowId)
        {
            string promptControlId = GetPromptControlId(rowId);
            string clickByIDCode = DriverMethodHelper.GetDriverMethodCode(MethodNames.ClickByID, StringHelper.Quote(promptControlId));
            builder.Append($"{clickByIDCode} // ");
        }

        public static void GenerateClickPromptCode(StringBuilder builder, string controlName, string rowId)
        {
            builder.AppendDriverMethod(MethodNames.ClickPrompt, controlName, rowId);
        }

        public static void GenerateSwitchFrame(StringBuilder builder)
        {
            _ = builder.AppendLine("&driver.SwitchFrame(\"index=0\") // should not be needed");
        }

        private static string GetPromptControlId(string rowId, int gridId = 1)
        {
            if (!int.TryParse(rowId, out int row))
                row = 1;

            // e.g.: "PROMPT_1_0002"
            return $"PROMPT_{gridId}_{row:D4}";
        }
    }
}
