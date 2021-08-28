using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters
{
    class ControlParm : ParameterGenerator
    {
        private readonly ControlValue ControlValue;

        public ControlParm(Parameter parm)
           : base(parm)
        {
            ValidateParameterTypes(parm, ParameterTypes.Control, typeof(ControlValue));
            ControlValue = parm.Value as ControlValue;
        }

        public override void Generate(StringBuilder builder)
        {
            _ = builder.AppendQuoted(ControlValue.Data.Name);
        }
    }
}
