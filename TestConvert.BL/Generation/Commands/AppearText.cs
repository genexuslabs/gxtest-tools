// Copyright 2021 GeneXus S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using GeneXus.GXtest.Tools.TestConvert.BL.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands
{
    class AppearText : CommandGenerator
    {
        public AppearText(Command command)
            : base(command)
        {
            Debug.Assert(command.Name == CommandNames.AppearText);
        }

        public override void Generate(StringBuilder builder)
        {
            builder.AppendCommentLine("Validation command generation", Verbosity.Diagnostic);

            builder.AppendCommentLine($"Ignoring first parm {Command.Parameters[0]}", Verbosity.Diagnostic);
            var stringParameters = new List<string>(Command.Parameters.Skip(1).Select(parm => ParameterHelper.GetParameterCode(parm)));

            stringParameters[0] = DriverHelper.GetDriverMethodCode(MethodNames.AppearText, stringParameters[0]);

            // avoid passing third parameter if empty
            const int messageParmIndex = 2;
            const string emptyQuotes = "\"\"";
            if (stringParameters[messageParmIndex] == emptyQuotes)
                stringParameters.RemoveAt(messageParmIndex);

            builder.AppendDriverMethod(MethodNames.Verify, stringParameters.ToArray());
        }
    }
}
