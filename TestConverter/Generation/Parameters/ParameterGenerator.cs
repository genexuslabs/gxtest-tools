using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters
{
    abstract class ParameterGenerator
    {
        protected Parameter Parameter { get; set; }

        protected ParameterGenerator(Parameter parm)
        {
            Parameter = parm;
        }

        public string ToCode()
        {
            var builder = new StringBuilder();
            Generate(builder);
            return builder.ToString();
        }

        public abstract void Generate(StringBuilder builder);
    }
}
