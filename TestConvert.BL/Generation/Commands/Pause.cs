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
using System;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands
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
