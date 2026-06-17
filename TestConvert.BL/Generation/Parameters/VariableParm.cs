// Copyright 2021 GeneXus S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Parameters
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
                throw new Exception($"Could not find value for variable '{VariableValue.VariableName}'.");

            return variable.Value;
        }
    }
}
