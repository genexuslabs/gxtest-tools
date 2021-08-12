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
            return command.Name switch
            {
                CommandNames.Go => new GoCommand(command),
                CommandNames.Click => new ClickCommand(command),
                CommandNames.FillInput => new FillInputCommand(command),
                CommandNames.AppearText => new AppearTextCommand(command),
                _ => new NotImplementedCommand(command),
            };
        }
    }
}
