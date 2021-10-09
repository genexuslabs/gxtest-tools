using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands
{
    class NotImplemented : CommandGenerator
    {
        public NotImplemented(Command command)
            : base(command)
        {
        }

        public override void Generate(StringBuilder builder)
        {
            builder.AppendCommentLine("Unknown command generation", Verbosity.Diagnostic);
            builder.AppendLine($"code not yet implemented: {Command}");
        }
    }
}
