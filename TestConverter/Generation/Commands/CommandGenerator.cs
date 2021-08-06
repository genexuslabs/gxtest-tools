using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    abstract class CommandGenerator
    {
        protected Command Command { get; set; }

        protected CommandGenerator(Command command)
        {
            Command = command;
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
