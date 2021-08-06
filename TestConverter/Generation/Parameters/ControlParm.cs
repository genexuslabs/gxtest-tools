using GeneXus.GXtest.Tools.TestConverter.v3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters
{
    class ControlParm : ParameterGenerator
    {
        private ParameterControlValue ControlValue;

        public ControlParm(Parameter parm)
           : base(parm)
        {
            ValidateParameterTypes(parm, ParameterTypes.Control, typeof(ParameterControlValue));
            ControlValue = parm.Value as ParameterControlValue;
        }

        public override void Generate(StringBuilder builder)
        {
            builder.AppendQuoted(ControlValue.Data.Name);
        }
    }
}
