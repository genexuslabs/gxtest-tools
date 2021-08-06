using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    class ClickCommand : CommandGenerator
    {
        public ClickCommand(Command command)
            : base(command)
        {
            Debug.Assert(command.Name == CommandNames.Click);
        }

        public override void Generate(StringBuilder builder)
        {
            builder.AppendCommentLine("Click command generation", Verbosity.Diagnostic);

            builder.AppendCommentLine($"Ignoring first parm {Command.Parameters[0]}", Verbosity.Diagnostic);
            builder.AppendDriverMethod(MethodNames.ClickBy, Command.Parameters[1]);
        }
    }
}
