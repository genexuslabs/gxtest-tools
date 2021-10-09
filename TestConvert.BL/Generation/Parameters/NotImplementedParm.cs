using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Parameters
{
    class NotImplementedParm : ParameterGenerator
    {
        public NotImplementedParm(Parameter parm)
            : base(parm)
        {
        }

        public override void Generate(StringBuilder builder)
        {
            builder.Append("parm not yet implemented");
        }
    }
}
