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
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation
{
    static class DriverHelper
    {
        private static readonly string parmSeparator = ", ";

        public static string DriverVar => "&driver";

        public static string GetDriverMethodCode(string methodName, params object[] parameters)
        {
            // eg: "&driver.Method(parm1, parm2, ..., paramN)"
            return $"{DriverVar}.{methodName}({string.Join(parmSeparator, parameters)})";
        }

        public static StringBuilder AppendDriverMethodNoParms(this StringBuilder builder, string methodName)
        {
            return builder.AppendDriverMethod(methodName, Array.Empty<string>());
        }

        public static StringBuilder AppendDriverMethod(this StringBuilder builder, string methodName, params object[] parameters)
        {

            return builder.AppendLine(GetDriverMethodCode(methodName, parameters));
        }

        public static StringBuilder AppendDriverMethod(this StringBuilder builder, string methodName, params Parameter[] parameters)
        {
            return builder.AppendDriverMethod(methodName, Array.ConvertAll(parameters, parm => ParameterHelper.GetParameterCode(parm)));
        }

        public static string GetVerifyCode(string hasValidationCode, bool expectsFalse, string message)
        {
            string negation = expectsFalse ? "not " : string.Empty;
            return GetDriverMethodCode(MethodNames.Verify, $"{negation}{ hasValidationCode}", "True", message);
        }

        public static bool GetExpectsFalse(Parameter parm)
        {
            string strNegateValue = ParameterHelper.GetParameterCode(parm);
            _ = bool.TryParse(strNegateValue, out bool expectsFalse);
            return expectsFalse;
        }
    }
}
