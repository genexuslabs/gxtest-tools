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
    class VerifyControlTextTable : TableCommand
    {
        private const int AdditionalParms = 4; // Reuses TargetControl position and adds 5

        public VerifyControlTextTable(Command command)
            : base(command, AdditionalParms)
        {
            Debug.Assert(command.Name == CommandNames.VerifyControlTextTable);
        }

        private int VerificationIndex => LastTableCommandParm; // The position for TargetControl is used by the start of the verification rule
        private ControlRuleValue Verification => Command.Parameters[VerificationIndex].Value as ControlRuleValue;

        private const int VerificationParmCount = 3; // rule, control, value

        private int NegateIndex => VerificationIndex + VerificationParmCount;

        private int ErrorMsgIndex => NegateIndex + 1;

        public override void Generate(StringBuilder builder)
        {
            // VerifyControlTextTable(
            //   [0] ignore        - BooleanValue[false],
            //   [1] grid          - ControlValue[b89750d5-63c9-484d-9722-9f468a41e753],
            //   [2] byRow         - RowSelectorValue[Row at parm[3]],
            //   [3] row           - LiteralValue[1],
            //   [4] verification  - ControlRuleValue[Control at parm[5] Equal(as String) Value at parm[6]],
            //   [5]               - ControlValue[7178a3cb-b9e8-4e26-a036-9d81567e862e],
            //   [6]               - LiteralValue[1],
            //   [7] negate        - BooleanValue[false],
            //   [8] errorMsg      - LiteralValue[])

            if (!PreGenerate(builder))
                return;

            bool expectsFalse = DriverHelper.GetExpectsFalse(Command.Parameters[NegateIndex]);
            string message = ParameterHelper.GetParameterCode(Command.Parameters[ErrorMsgIndex]);
            builder.AppendLine(DriverHelper.GetVerifyCode(GetComparisonExpression(), expectsFalse, message));
        }

        private string GetComparisonExpression()
        {
            var selectorHelper = new ControlRuleHelper(Command, Verification);
            return selectorHelper.GetComparisonExpression(RowExpression);
        }
    }
}
