using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    class PressKey : CommandGenerator
    {
        public PressKey(Command command)
            : base(command)
        {
            Debug.Assert(command.Name == CommandNames.PressKey);
        }

        public override void Generate(StringBuilder builder)
        {
            builder.AppendCommentLine("PressKey command generation", Verbosity.Diagnostic);
            builder.AppendCommentLine($"Ignoring first parm {Command.Parameters[0]}", Verbosity.Diagnostic);

            string parameter = ParameterHelper.GetParameterCode(Command.Parameters[1]);
            foreach (var fragment in StringHelper.SplitLiteralByKeys(parameter))
                builder.AppendDriverMethod(MethodNames.SendKeys, fragment);
        }
    }
}
