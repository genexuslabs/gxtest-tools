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
using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands
{
    class AppearBalloonTable : TableCommand
    {
        private const int AdditionalParms = 2; // negate, error

        public AppearBalloonTable(Command command)
            : base(command, AdditionalParms)
        {
            Debug.Assert(command.Name == CommandNames.AppearBalloonTable);
        }

        protected int NegateIndex => LastTableCommandParm + 1;
        protected int ErrorMsgIndex => NegateIndex + 1;

        public override void Generate(StringBuilder builder)
        {
            // Validation AppearBalloonTable(
            //  [0] ignore        - ParameterBooleanValue[false],
            //  [1] grid          - ParameterControlValue[a4126e3c-555b-4570-86c5-da96ec7da727],
            //  [2] byRow         - /* SelectionByRow */ RowSelectorValue,
            //  [3] row           - ParameterLiteralValue[1],
            //  [4] targetControl - ParameterControlValue[401bbfb2-14de-4e7c-b79b-c2c0aaf12d0f],
            //  [5] negate?       - ParameterBooleanValue[false],
            //  [6} errorMsg      - ParameterLiteralValue[No matching 'Country'.])

            if (!PreGenerate(builder))
                return;

            builder.AppendHasValidation(Command.Parameters[NegateIndex], Command.Parameters[ErrorMsgIndex], TargetControlName, Row);
        }

        protected override bool PreGenerate(StringBuilder builder)
        {
            if (!base.PreGenerate(builder))
                return false;

            if (!UsesRowSelector)
            {
                builder.AppendLine("code not yet implemented");
                return false;
            }

            return true;
        }
    }
}
