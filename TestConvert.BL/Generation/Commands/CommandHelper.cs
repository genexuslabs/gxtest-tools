// Copyright 2021 GeneXus S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
