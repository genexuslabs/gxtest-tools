using System.Collections.Generic;

namespace GeneXus.GXtest.Tools.TestConvert.BL.v3
{
    public class Element
    {
        private readonly SortedList<int, Command> commands = new SortedList<int, Command>();

        internal void AddCommand(Command command)
        {
            commands.Add(command.Order, command);
        }

        internal IList<Command> GetCommands()
        {
            return commands.Values;
        }

        internal void AddCommandParameter(Parameter parm)
        {
            Command command = commands[parm.ParentOrder];
            command.AddParameter(parm);
        }

        internal void AddCommandParameterValue(ParameterValue val)
        {
            Command command = commands[val.ParentOrder];
            command.AddParameterValue(val);
        }
    }
}
