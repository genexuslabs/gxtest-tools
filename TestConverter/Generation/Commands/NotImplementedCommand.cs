using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    class NotImplementedCommand : CommandGenerator
    {
        public NotImplementedCommand(Command command)
            : base(command)
        {
        }

        public override void Generate(StringBuilder builder)
        {
            builder.AppendCommentLine("Unknown command generation", Verbosity.Diagnostic);
            builder.AppendLine("code not yet implemented");
        }
    }
}
