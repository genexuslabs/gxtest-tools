using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation
{
    static class DriverMethodHelper
    {
        private static readonly string driverVar = "&driver";
        private static readonly string parmSeparator = ", ";

        public static string GetDriverMethodCode(string methodName, params string[] parameters)
        {
            // eg: "&driver.Method(parm1, parm2, ..., paramN)"
            return $"{driverVar}.{methodName}({string.Join(parmSeparator, parameters)})";
        }

        public static StringBuilder AppendDriverMethodNoParms(this StringBuilder builder, string methodName)
        {
            return builder.AppendDriverMethod(methodName, Array.Empty<string>());
        }

        public static StringBuilder AppendDriverMethod(this StringBuilder builder, string methodName, params string[] parameters)
        {

            return builder.AppendLine(GetDriverMethodCode(methodName, parameters));
        }

        public static StringBuilder AppendDriverMethod(this StringBuilder builder, string methodName, params Parameter[] parameters)
        {
            return builder.AppendDriverMethod(methodName, Array.ConvertAll(parameters, parm => ParameterHelper.GetParameterCode(parm)));
        }
    }
}
