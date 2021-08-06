using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    public static class CommandHelper
    {
        public static string GetCommandCode(Command command)
        {
            var builder = new StringBuilder();
            builder.AppendCommand(command);
            return builder.ToString();
        }

        public static void AppendCommand(this StringBuilder builder, Command command)
        {
            builder.AppendCommentLine($"{command}", Verbosity.Detailed);
            CommandGenerator generator = CreateGenerator(command);
            generator.Generate(builder);
        }

        private static CommandGenerator CreateGenerator(Command command)
        {
            switch (command.Name)
            {
                case CommandNames.Go:
                    return new GoCommand(command);

                case CommandNames.Click:
                    return new ClickCommand(command);

                default:
                    return new NotImplementedCommand(command);
            }
        }
    }
}
