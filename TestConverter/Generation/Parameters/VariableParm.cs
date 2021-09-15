using GeneXus.GXtest.Tools.TestConverter.v3;
using System;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters
{
    class VariableParm : ParameterGenerator
    {
        private readonly VariableValue VariableValue;

        public VariableParm(Parameter parm)
           : base(parm)
        {
            ValidateParameterTypes(parm, ParmType.Variable, typeof(VariableValue));
            VariableValue = parm.Value as VariableValue;
        }

        public override void Generate(StringBuilder builder)
        {
            _ = builder.Append(GetActualValue());
        }

        private string GetActualValue()
        {
            if (!GenerationOptions.General.TryGetVariable(VariableValue.VariableName, out GenerationOptions.Variable variable))
                throw new Exception($"Could not find value for variable '{VariableValue.VariableName}'");

            return variable.Value;
        }
    }
}
