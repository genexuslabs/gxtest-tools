using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters
{
    public static class ParameterHelper
    {
        public static string GetParameterCode(Parameter parm)
        {
            var builder = new StringBuilder();
            builder.AppendParameter(parm);
            return builder.ToString();
        }

        public static void AppendParameter(this StringBuilder builder, Parameter parm)
        {
            builder.Append("the parameter value");
        }
    }
}
