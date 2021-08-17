using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    class FillInputTableCommand : CommandGenerator
    {
        public FillInputTableCommand(Command command)
            : base(command)
        {
            Debug.Assert(command.Name == CommandNames.FillInputTable);
        }
        public override void Generate(StringBuilder builder)
        {
            builder.AppendCommentLine("FillInputTable command generation", Verbosity.Diagnostic);
            builder.AppendCommentLine($"Ignoring first parm {Command.Parameters[0]}", Verbosity.Diagnostic);

            string selectionType = Command.Parameters[2].Type;
            if (selectionType != SelectionType.ByRow)
            {
                builder.AppendLine("code not yet implemented");
                return;
            }

            if (Command.Parameters.Count < 6)
            {
                builder.AppendLine("not enough parameters");
                return;
            }

            string rowId = StringHelper.RemoveQuotes(ParameterHelper.GetParameterCode(Command.Parameters[3]));
            string controlName = ParameterHelper.GetParameterCode(Command.Parameters[4]);
            string valueToType = ParameterHelper.GetParameterCode(Command.Parameters[5]);

            builder.AppendDriverMethod(MethodNames.Type, controlName, rowId, valueToType);
        }
    }
}
