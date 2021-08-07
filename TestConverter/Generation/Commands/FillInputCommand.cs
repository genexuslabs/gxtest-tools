using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    class FillInputCommand : CommandGenerator
    {
        public FillInputCommand(Command command)
            : base(command)
        {
            Debug.Assert(command.Name == CommandNames.FillInput);
        }

        public override void Generate(StringBuilder builder)
        {
            builder.AppendCommentLine("FillInput command generation", Verbosity.Diagnostic);
            builder.AppendCommentLine($"Ignoring first parm {Command.Parameters[0]}", Verbosity.Diagnostic);

            builder.AppendDriverMethod(MethodNames.TypeByID, Command.Parameters[1], Command.Parameters[2]);
        }
    }
}
