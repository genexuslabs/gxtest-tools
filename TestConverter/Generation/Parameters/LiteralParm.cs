using GeneXus.GXtest.Tools.TestConverter.v3;
using System;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters
{
    class LiteralParm : ParameterGenerator
    {
        private ParameterLiteralValue LiteralValue;
        
        public LiteralParm(Parameter parm)
           : base(parm)
        {
            ValidateParameterTypes(parm, ParameterTypes.Literal, typeof(ParameterLiteralValue));
            LiteralValue = parm.Value as ParameterLiteralValue;
        }
        
        public override void Generate(StringBuilder builder)
        {
            builder.AppendQuoted(LiteralValue.Value);
        }
    }
}
