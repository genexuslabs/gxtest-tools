using GeneXus.GXtest.Tools.TestConverter.Generation.Helpers;
using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
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
