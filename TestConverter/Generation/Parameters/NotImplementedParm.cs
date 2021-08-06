using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters
{
    class NotImplementedParm : ParameterGenerator
    {
        public NotImplementedParm(Parameter parm)
            : base(parm)
        {
        }

        public override void Generate(StringBuilder builder)
        {
            builder.AppendLine("parm not yet implemented");
        }
    }
}
