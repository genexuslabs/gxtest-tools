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
            ParameterGenerator generator = CreateGenerator(parm);
            generator.Generate(builder);
        }
        private static ParameterGenerator CreateGenerator(Parameter parm)
        {
            return parm.Type switch
            {
                ParmType.Literal => new LiteralParm(parm),
                ParmType.Control => new ControlParm(parm),
                ParmType.Boolean => new BooleanParm(parm),
                ParmType.Variable => new VariableParm(parm),
                _ => new NotImplementedParm(parm),
            };
        }
    }
}
