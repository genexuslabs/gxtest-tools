using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation
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
