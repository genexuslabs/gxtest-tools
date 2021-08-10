using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters
{
    class VariableParm : ParameterGenerator
    {
        private readonly ParameterVariableValue ControlValue;

        public VariableParm(Parameter parm)
           : base(parm)
        {
            ValidateParameterTypes(parm, ParameterTypes.Variable, typeof(ParameterVariableValue));
            ControlValue = parm.Value as ParameterVariableValue;
        }

        public override void Generate(StringBuilder builder)
        {
            _ = builder.Append($"&{ControlValue.VariableName}.Link()");
        }
    }
}
