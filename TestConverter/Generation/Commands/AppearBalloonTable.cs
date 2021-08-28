using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    class AppearBalloonTable : CommandGenerator
    {
        public AppearBalloonTable(Command command)
            : base(command)
        {
            Debug.Assert(command.Name == CommandNames.AppearBalloonTable);
        }

        public override void Generate(StringBuilder builder)
        {
            // Validation AppearBalloonTable(
            //  [0] ignore    - ParameterBooleanValue[false],
            //  [1] grid      - ParameterControlValue[a4126e3c-555b-4570-86c5-da96ec7da727],
            //  [2] byRow     - /* SelectionByRow */ null,
            //  [3] row       - ParameterLiteralValue[1],
            //  [4] CountryId - ParameterControlValue[401bbfb2-14de-4e7c-b79b-c2c0aaf12d0f],
            //  [5] negate?   - ParameterBooleanValue[false],
            //  [6} errorMsg  - ParameterLiteralValue[No matching 'Country'.])

            builder.AppendCommentLine("AppearBalloonTable command generation", Verbosity.Diagnostic);
            builder.AppendCommentLine($"Ignoring first parm {Command.Parameters[0]}", Verbosity.Diagnostic);
            builder.AppendCommentLine($"Ignoring second parm {Command.Parameters[1]}", Verbosity.Diagnostic);

            string selectionType = Command.Parameters[2].Type;
            if (selectionType != SelectionType.ByRow)
            {
                builder.AppendLine("code not yet implemented");
                return;
            }

            if (Command.Parameters.Count < 7)
            {
                builder.AppendLine("not enough parameters");
                return;
            }

            // Expected:  &driver.Verify(&driver.GetTextByID("COUNTRYID_0001_Balloon") <> \"\", True, "No matching 'Country'.")
            // Desired:   &driver.Verify(&driver.HasValidationText("CountryId", 1), True, "No matching 'Country'.")

            string rowId = StringHelper.RemoveQuotes(ParameterHelper.GetParameterCode(Command.Parameters[3]));
            string controlName = ParameterHelper.GetParameterCode(Command.Parameters[4]);
            string hasValidation = DriverMethodHelper.GetDriverMethodCode(MethodNames.HasValidationText, controlName, rowId);
            string hasValidationWorkaround = GetHasValidationWorkAround(controlName, rowId);
            string expectedResult = GetExpectedResult(Command.Parameters[5]);
            string message = ParameterHelper.GetParameterCode(Command.Parameters[6]);

            builder.Append(GetVerifyCode(hasValidationWorkaround, expectedResult, message));
            builder.AppendLine($" // {GetVerifyCode(hasValidation, expectedResult, message)}");

            // builder.AppendDriverMethod(MethodNames.Verify, hasValidation, expectedResult, message);
        }

        private static string GetVerifyCode(string hasValidationCode, string expectedResult, string message)
        {
            return DriverMethodHelper.GetDriverMethodCode(MethodNames.Verify, hasValidationCode, expectedResult, message);
        }

        private static string GetHasValidationWorkAround(string controlName, string rowId)
        {
            string balloonControlId = GetBalloonControlId(controlName, rowId);
            return $"{DriverMethodHelper.GetDriverMethodCode(MethodNames.GetTextByID, StringHelper.Quote(balloonControlId))} <> \"\"";
        }

        private static string GetBalloonControlId(string controlName, string rowId)
        {
            if (!int.TryParse(rowId, out int row))
                row = 1;

            return $"{StringHelper.RemoveQuotes(controlName.ToUpper())}_{row:D4}_Balloon";
        }

        private static string GetExpectedResult(Parameter parm)
        {
            string strNegateValue = ParameterHelper.GetParameterCode(parm);
            _ = bool.TryParse(strNegateValue, out bool negateValue);
            return negateValue ? "False" : "True";
        }
    }
}
