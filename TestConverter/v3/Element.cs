using System.Collections.Generic;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class Element
    {
        private SortedList<string, Command> commands = new SortedList<string, Command>();

        internal void AddCommand(Command command)
        {
            commands.Add(command.Order, command);
        }

        internal IEnumerable<Command> GetCommands()
        {
            return commands.Values;
        }

        internal void AddCommandParameter(Parameter parm)
        {
            Command command = commands[parm.ParentOrder];
            command.AddParameter(parm);
        }

        internal void AddCommandParameterValue(ParameterValue val, TestCase testCase)
        {
            Command command = commands[val.ParentOrder];
            command.AddParameterValue(val);
        }
    }
}
