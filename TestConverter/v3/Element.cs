using System.Collections.Generic;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class Element
    {
        private SortedList<string, Command> commands = new SortedList<string, Command>();

        internal void AddCommand(Command command, TestCase testCase)
        {
            commands.Add(command.Order, command);
        }

        protected IEnumerable<Command> GetCommands()
        {
            return commands.Values;
        }

        internal void AddCommandParameter(Parameter parm, TestCase testCase)
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
