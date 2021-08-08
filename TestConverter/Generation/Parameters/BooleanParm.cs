using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters
{
    class BooleanParm : ParameterGenerator
    {
        private readonly ParameterBooleanValue BooleanValue;

        public BooleanParm(Parameter parm)
           : base(parm)
        {
            ValidateParameterTypes(parm, ParameterTypes.Boolean, typeof(ParameterBooleanValue));
            BooleanValue = parm.Value as ParameterBooleanValue;
        }

        public override void Generate(StringBuilder builder)
        {
            if (!bool.TryParse(BooleanValue.Value, out bool value))
                value = false;

            _ = builder.Append(value);
        }
    }
}
