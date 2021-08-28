using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters
{
    class LiteralParm : ParameterGenerator
    {
        private readonly LiteralValue LiteralValue;

        public LiteralParm(Parameter parm)
           : base(parm)
        {
            ValidateParameterTypes(parm, ParameterTypes.Literal, typeof(LiteralValue));
            LiteralValue = parm.Value as LiteralValue;
        }

        public override void Generate(StringBuilder builder)
        {
            _ = builder.AppendQuoted(LiteralValue.Value);
        }
    }
}
