using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands
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
                case CommandNames.AppearBalloon:
                    return new AppearBalloon(command);
                case CommandNames.AppearBalloonTable:
                    return new AppearBalloonTable(command);
                case CommandNames.AppearText:
					return new AppearText(command);
                case CommandNames.Click:
					return new Click(command);
                case CommandNames.ClickPromptTable:
					return new ClickPromptTable(command);
                case CommandNames.ClickTable:
					return new ClickTable(command);
                case CommandNames.FillInput:
					return new FillInput(command);
                case CommandNames.FillInputTable:
					return new FillInputTable(command);
                case CommandNames.Go:
					return new Go(command);
                case CommandNames.Pause:
					return new Pause(command);
                case CommandNames.PressKey:
					return new PressKey(command);
                case CommandNames.VerifyControlText:
					return new VerifyControlText(command);
                case CommandNames.VerifyControlTextTable:
					return new VerifyControlTextTable(command);
                default:
					return new NotImplemented(command);
            }
        }
    }
}
