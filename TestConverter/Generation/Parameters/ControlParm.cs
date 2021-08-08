using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters
{
    class ControlParm : ParameterGenerator
    {
        private readonly ParameterControlValue ControlValue;

        public ControlParm(Parameter parm)
           : base(parm)
        {
            ValidateParameterTypes(parm, ParameterTypes.Control, typeof(ParameterControlValue));
            ControlValue = parm.Value as ParameterControlValue;
        }

        public override void Generate(StringBuilder builder)
        {
            _ = builder.AppendQuoted(ControlValue.Data.Name);
        }
    }
}
