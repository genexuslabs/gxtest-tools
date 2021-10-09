using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Parameters
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
            switch (parm.Type)
            {
                case ParmType.Literal:
					return new LiteralParm(parm);
                case ParmType.Control:
                    return new ControlParm(parm);
                case ParmType.Boolean:
                    return new BooleanParm(parm);
                case ParmType.Variable:
                    return new VariableParm(parm);
                default:
                    return new NotImplementedParm(parm);
            }
        }

        public static int GetNumericValue(Command command, int parmIndex) => GetNumericValue(command.Parameters[parmIndex]);

        public static int GetNumericValue(Parameter parm)
        {
            string numberAsString = StringHelper.RemoveQuotes(ParameterHelper.GetParameterCode(parm));

            if (!int.TryParse(numberAsString, out int number))
                return 0;

            return number;
        }

        public static string GetStringValue(Command command, int parmIndex) => GetStringValue(command.Parameters[parmIndex]);

        public static string GetStringValue(Parameter parm)
        {
            return GetParameterCode(parm);
        }
    }
}
