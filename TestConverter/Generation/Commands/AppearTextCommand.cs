﻿using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Collections.Generic;
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
            List<string> stringParameters = new(Command.Parameters.Skip(1).Select(parm => ParameterHelper.GetParameterCode(parm)));

            stringParameters[0] = DriverMethodHelper.GetDriverMethodCode(MethodNames.AppearText, stringParameters[0]);

            // avoid passing third parameter if empty
            const int messageParmIndex = 2;
            const string emptyQuotes = "\"\"";
            if (stringParameters[messageParmIndex] == emptyQuotes)
                stringParameters.RemoveAt(messageParmIndex);

            builder.AppendDriverMethod(MethodNames.Verify, stringParameters.ToArray());
        }
    }
}
