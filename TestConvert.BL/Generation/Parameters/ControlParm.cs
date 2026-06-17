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
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Parameters
{
    class ControlParm : ParameterGenerator
    {
        private readonly ControlValue ControlValue;

        public ControlParm(Parameter parm)
           : base(parm)
        {
            ValidateParameterTypes(parm, ParmType.Control, typeof(ControlValue));
            ControlValue = parm.Value as ControlValue;
        }

        public override void Generate(StringBuilder builder)
        {
            _ = builder.AppendQuoted(ControlName);
        }

        private bool IsVariable
        {
            get
            {
                const string variableControlClass = "Variable";
                return string.Compare(ControlValue.Data.Class, variableControlClass, /* ignoreCase */ true) == 0;
            }
        }

        private string ControlName
        {
            get
            {
                return $"{(IsVariable ? "&" : "")}{ControlValue.Data.Name}";
            }
        }
    }
}
