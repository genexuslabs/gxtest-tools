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
            Debug.Assert(parm.Type == ParameterTypes.Literal);
            Debug.Assert(parm.Value != null);
            Debug.Assert(parm.Value is ParameterLiteralValue);

            if (parm.Value == null)
                throw new Exception("ParameterLiteralValue contains no value");

            LiteralValue = parm.Value as ParameterLiteralValue;
            if (LiteralValue == null)
                throw new Exception($"ParameterLiteralValue contains invalid value. Expected {typeof(ParameterLiteralValue)}; found {parm.Value.GetType()}.");
        }
        
        public override void Generate(StringBuilder builder)
        {
            builder.Append($"\"{LiteralValue.Value}\"");
        }
    }
}
