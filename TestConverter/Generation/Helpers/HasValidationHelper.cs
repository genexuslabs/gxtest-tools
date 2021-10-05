
using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Helpers
{
    static class HasValidationHelper
    {
        public static StringBuilder AppendHasValidation(this StringBuilder builder, Parameter expectsFalseParm, Parameter errorMsgParm, string controlName, int? row = null)
        {
            // Expected:  &driver.Verify(&driver.GetTextByID("COUNTRYID_0001_Balloon") <> \"\", True, "No matching 'Country'.")
            // Desired:   &driver.Verify(&driver.HasValidationText("CountryId", 1), True, "No matching 'Country'.")

            string hasValidation = GetHasValidation(controlName, row);
            string hasValidationWorkaround = GetHasValidationWorkAround(controlName, row);
            bool expectsFalse = DriverHelper.GetExpectsFalse(expectsFalseParm);
            string message = ParameterHelper.GetParameterCode(errorMsgParm);

            _ = builder.Append(DriverHelper.GetVerifyCode(hasValidationWorkaround, expectsFalse, message));
            return builder.AppendLine($" // {DriverHelper.GetVerifyCode(hasValidation, expectsFalse, message)}");

            // When workaround stops being needed we will just do
            // return builder.AppendDriverMethod(MethodNames.Verify, hasValidation, expectedResult, message);
        }

        private static string GetHasValidation(string controlName, int? row = null)
        {
            return (row == null) ? DriverHelper.GetDriverMethodCode(MethodNames.HasValidationText, controlName)
                : DriverHelper.GetDriverMethodCode(MethodNames.HasValidationText, controlName, row);
        }

        private static string GetHasValidationWorkAround(string controlName, int? row = null)
        {
            string rowSpec = row != null ? $"_{row:D4}" : string.Empty;
            return GetHasValidationWorkAround(controlName, rowSpec);
        }

        private static string GetHasValidationWorkAround(string controlName, string rowSpec = "")
        {
            string balloonControlId = GetBalloonControlId(controlName, rowSpec);
            return DriverHelper.GetDriverMethodCode(MethodNames.IsElementPresentByID, StringHelper.Quote(balloonControlId));
        }

        private static string GetBalloonControlId(string controlName, string rowSpec = "")
        {
            string balloonSuffix = "_Balloon";
            string rootName = StringHelper.RemoveQuotes(controlName.ToUpper());
            if (rootName.StartsWith('&'))
                rootName = "v" + rootName[1..];

            return $"{rootName}{rowSpec}{balloonSuffix}";
        }
    }
}
