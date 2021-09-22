using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
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

            // Expected:  &driver.Verify(&driver.GetTextByID("COUNTRYID_0001_Balloon") <> \"\", True, "No matching 'Country'.")
            // Desired:   &driver.Verify(&driver.HasValidationText("CountryId", 1), True, "No matching 'Country'.")

            string hasValidation = DriverHelper.GetDriverMethodCode(MethodNames.HasValidationText, TargetControlName, Row);
            string hasValidationWorkaround = GetHasValidationWorkAround(TargetControlName, Row);
            bool expectsFalse = DriverHelper.GetExpectsFalse(Command.Parameters[NegateIndex]);
            string message = ParameterHelper.GetParameterCode(Command.Parameters[ErrorMsgIndex]);

            builder.Append(DriverHelper.GetVerifyCode(hasValidationWorkaround, expectsFalse, message));
            builder.AppendLine($" // {DriverHelper.GetVerifyCode(hasValidation, expectsFalse, message)}");

            // When workaround stops being needed we will just do
            // builder.AppendDriverMethod(MethodNames.Verify, hasValidation, expectedResult, message);
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

        private static string GetHasValidationWorkAround(string controlName, int row)
        {
            string balloonControlId = GetBalloonControlId(controlName, row);
            return DriverHelper.GetDriverMethodCode(MethodNames.IsElementPresentByID, StringHelper.Quote(balloonControlId));
        }

        private static string GetBalloonControlId(string controlName, int row)
        {
            return $"{StringHelper.RemoveQuotes(controlName.ToUpper())}_{row:D4}_Balloon";
        }
    }
}
