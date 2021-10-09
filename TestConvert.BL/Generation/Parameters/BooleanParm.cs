using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Parameters
{
    class BooleanParm : ParameterGenerator
    {
        private readonly BooleanValue BooleanValue;

        public BooleanParm(Parameter parm)
           : base(parm)
        {
            ValidateParameterTypes(parm, ParmType.Boolean, typeof(BooleanValue));
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
