using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    class AppearTextCommand : CommandGenerator
    {
        public AppearTextCommand(Command command)
            : base(command)
        {
            Debug.Assert(command.Name == CommandNames.AppearText);
        }

        public override void Generate(StringBuilder builder)
        {
            builder.AppendCommentLine("Validation command generation", Verbosity.Diagnostic);

            builder.AppendCommentLine($"Ignoring first parm {Command.Parameters[0]}", Verbosity.Diagnostic);
            string[] stringParameters = Command.Parameters.Skip(1).Select(parm => ParameterHelper.GetParameterCode(parm)).ToArray();

            stringParameters[0] = DriverMethodHelper.GetDriverMethodCode(MethodNames.AppearText, stringParameters[0]);

            builder.AppendDriverMethod(MethodNames.Verify, stringParameters);
        }
    }
}
