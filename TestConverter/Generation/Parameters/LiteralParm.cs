using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters
{
    class LiteralParm : ParameterGenerator
    {
        private readonly ParameterLiteralValue LiteralValue;

        public LiteralParm(Parameter parm)
           : base(parm)
        {
            ValidateParameterTypes(parm, ParameterTypes.Literal, typeof(ParameterLiteralValue));
            LiteralValue = parm.Value as ParameterLiteralValue;
        }

        public override void Generate(StringBuilder builder)
        {
            _ = builder.AppendQuoted(LiteralValue.Value);
        }
    }
}
