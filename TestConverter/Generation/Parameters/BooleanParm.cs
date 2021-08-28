using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters
{
    class BooleanParm : ParameterGenerator
    {
        private readonly BooleanValue BooleanValue;

        public BooleanParm(Parameter parm)
           : base(parm)
        {
            ValidateParameterTypes(parm, ParameterTypes.Boolean, typeof(BooleanValue));
            BooleanValue = parm.Value as BooleanValue;
        }

        public override void Generate(StringBuilder builder)
        {
            if (!bool.TryParse(BooleanValue.Value, out bool value))
                value = false;

            _ = builder.Append(value);
        }
    }
}
