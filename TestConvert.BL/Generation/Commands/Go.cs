using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands
{
    class Go : CommandGenerator
    {
        public Go(Command command)
            : base(command)
        {
            Debug.Assert(command.Name == CommandNames.Go);
        }

        public override void Generate(StringBuilder builder)
        {
            builder.AppendCommentLine("GO command generation", Verbosity.Diagnostic);
            builder.AppendCommentLine($"Ignoring first parm {Command.Parameters[0]}", Verbosity.Diagnostic);

            builder.AppendDriverMethod(MethodNames.Go, Command.Parameters[1]);
        }
    }
}
