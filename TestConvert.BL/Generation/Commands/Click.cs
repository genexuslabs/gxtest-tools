using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands
{
    class Click : CommandGenerator
    {
        public Click(Command command)
            : base(command)
        {
            Debug.Assert(command.Name == CommandNames.Click);
        }

        public override void Generate(StringBuilder builder)
        {
            builder.AppendCommentLine("Click command generation", Verbosity.Diagnostic);
            builder.AppendCommentLine($"Ignoring first parm {Command.Parameters[0]}", Verbosity.Diagnostic);

            builder.AppendDriverMethod(MethodNames.Click, Command.Parameters[1]);
        }
    }
}
