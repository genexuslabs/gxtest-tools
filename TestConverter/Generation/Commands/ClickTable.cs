using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    class ClickTable : CommandGenerator
    {
        public ClickTable(Command command)
            : base(command)
        {
            Debug.Assert(command.Name == CommandNames.ClickTable);
        }

        public override void Generate(StringBuilder builder)
        {
            builder.AppendCommentLine("ClickTable command generation", Verbosity.Diagnostic);
            builder.AppendCommentLine($"Ignoring first parm {Command.Parameters[0]}", Verbosity.Diagnostic);

            string selectionType = Command.Parameters[2].Type;
            if (selectionType != SelectionType.ByRow)
            {
                builder.AppendLine("code not yet implemented");
                return;
            }

            if (Command.Parameters.Count < 5)
            {
                builder.AppendLine("not enough parameters");
                return;
            }

            string gridName = ParameterHelper.GetParameterCode(Command.Parameters[1]);
            string rowId = StringHelper.RemoveQuotes(ParameterHelper.GetParameterCode(Command.Parameters[3]));
            string controlName = ParameterHelper.GetParameterCode(Command.Parameters[4]);

            if (IsDeleteRow(gridName, controlName))
            {
                GenerateDeleteRow(builder, gridName, rowId);
                return;
            }

            builder.AppendDriverMethod(MethodNames.Click, controlName, rowId);
        }

        private static bool IsDeleteRow(string gridName, string controlName)
        {
            return string.Compare(controlName, $"delete{gridName}", true) != 0;
        }

        private static void GenerateDeleteRow(StringBuilder builder, string gridName, string rowId)
        {
            GenerateDeleteRowCodeWorkAround(builder, gridName, rowId);
            GenerateDeleteRowCode(builder, gridName, rowId);
        }

        public static void GenerateDeleteRowCodeWorkAround(StringBuilder builder, string gridName, string rowId)
        {
            string deleteRowControlId = GetDeleteRowControlId(gridName, rowId);
            string clickByIDCode = DriverMethodHelper.GetDriverMethodCode(MethodNames.ClickByID, deleteRowControlId);
            builder.Append($"{clickByIDCode} // ");
        }

        public static void GenerateDeleteRowCode(StringBuilder builder, string gridName, string rowId)
        {
            builder.AppendDriverMethod(MethodNames.DeleteRow, gridName, rowId);
        }

        private static string GetDeleteRowControlId(string quotedGridName, string rowId)
        {
            if (!int.TryParse(rowId, out int row))
                row = 1;

            string gridName = StringHelper.RemoveQuotes(quotedGridName);
            string titleCaseGridName = gridName.Length <= 0 ? string.Empty : Char.ToUpper(gridName[0]) + gridName[1..].ToLower();

            return $"\"delete{StringHelper.RemoveQuotes(titleCaseGridName)}_{row:D4}\"";
        }
    }
}
