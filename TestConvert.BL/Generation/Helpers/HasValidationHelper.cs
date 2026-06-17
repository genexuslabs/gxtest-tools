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
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Helpers
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
            if (rootName.StartsWith("&"))
                rootName = $"v{rootName.Substring(1)}";

            return $"{rootName}{rowSpec}{balloonSuffix}";
        }
    }
}
