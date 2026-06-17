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

using GeneXus.GXtest.Tools.TestConvert.BL.Generation.Helpers;
using GeneXus.GXtest.Tools.TestConvert.BL.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands
{
    class AppearBalloon : CommandGenerator
    {
        public AppearBalloon(Command command)
            : base(command)
        {
            Debug.Assert(command.Name == CommandNames.AppearBalloon);
        }

        private static int TargetControlIndex => 1;
        private static int NegateIndex => 2;
        private static int ErrorMsgIndex => 3;

        private string TargetControlName => ParameterHelper.GetParameterCode(Command.Parameters[TargetControlIndex]);

        public override void Generate(StringBuilder builder)
        {
            // Validation AppearBalloon(
            //  [0] ignore        - BooleanValue[false],
            //  [1] targetControl - ControlValue[8ff7df6c-3a86-4e1f-8423-a506eebe727c],
            //  [2] negate?       - BooleanValue[true],
            //  [3] errorMsg      - LiteralValue[The value is not a valid number.])

            builder.AppendHasValidation(Command.Parameters[NegateIndex], Command.Parameters[ErrorMsgIndex], TargetControlName);
        }
    }
}
