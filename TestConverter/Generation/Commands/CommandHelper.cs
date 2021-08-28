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
                CommandNames.AppearBalloonTable => new AppearBalloonTable(command),
                CommandNames.AppearText => new AppearText(command),
                CommandNames.Click => new Click(command),
                CommandNames.ClickPromptTable => new ClickPromptTable(command),
                CommandNames.ClickTable => new ClickTable(command),
                CommandNames.FillInput => new FillInput(command),
                CommandNames.FillInputTable => new FillInputTable(command),
                CommandNames.Go => new Go(command),
                CommandNames.Pause => new Pause(command),
                CommandNames.PressKey => new PressKey(command),
                _ => new NotImplemented(command),
            };
        }
    }
}
