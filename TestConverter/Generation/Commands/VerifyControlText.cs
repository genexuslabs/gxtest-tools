using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    class VerifyControlText : ControlCommand
    {
        private const int AdditionalParms = 2; // negate, errorMsg

        public VerifyControlText(Command command)
            : base(command, AdditionalParms)
        {
            Debug.Assert(command.Name == CommandNames.VerifyControlText);
        }

        protected int NegateIndex => LastSelectionParm + 1;
        protected int ErrorMsgIndex => NegateIndex + 1;

        public override void Generate(StringBuilder builder)
        {
            // VerifyControlText(
            // [0] ignore    - BooleanValue[false]
            // [1] byControl - ControlRuleValue[Control at parm[2] Equal(as String) Value at parm[3]]
            // [2] control   - ControlValue[33935f58-45a9-467f-898f-e4d127293861]
            // [3] value     - LiteralValue[Data has been successfully updated.]
            // [4] negate    - BooleanValue[false]
            // [5] errorDesc - LiteralValue[]

            if (!PreGenerate(builder))
                return;

            // Expected (text): &driver.Verify(CompareTextValue(&driver.GetText("ErrorViewer"), TextComparison.StartsWith, "Data has been succesfully updated."))
            // Expected (number): &driver.Verify([not ]Compare(&driver.GetText("CountryId").ToNumeric(), CompareKind.Equal, 42))

            bool expectsFalse = DriverHelper.GetExpectsFalse(Command.Parameters[NegateIndex]);
            string message = ParameterHelper.GetParameterCode(Command.Parameters[ErrorMsgIndex]);
            builder.Append(DriverHelper.GetVerifyCode(GetComparisonExpression(), expectsFalse, message));
        }
    }
}
