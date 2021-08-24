using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    class Pause : CommandGenerator
    {
        public Pause(Command command)
            : base(command)
        {
            Debug.Assert(command.Name == CommandNames.Pause);
        }

        public override void Generate(StringBuilder builder)
        {
            builder.AppendCommentLine("Pause command generation", Verbosity.Diagnostic);

            int seconds = 1;

            string stringMilliseconds = StringHelper.RemoveQuotes(ParameterHelper.GetParameterCode(Command.Parameters[0]));
            if (int.TryParse(stringMilliseconds, out int milliseconds))
                seconds = Math.Max(1, milliseconds / 1000);

            builder.AppendDriverMethod(MethodNames.PauseFor, seconds.ToString());
        }
    }
}
