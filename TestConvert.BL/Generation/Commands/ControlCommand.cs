using GeneXus.GXtest.Tools.TestConvert.BL.Generation.Helpers;
using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands
{
    abstract class ControlCommand : CommandGenerator
    {
        // Control
        // [0] ignore    - BooleanValue[false]
        // [1] byControl - ControlRuleValue[Control at parm[2] Equal(as String) Value at parm[3]]
        // [2] control   - ControlValue[33935f58-45a9-467f-898f-e4d127293861]
        // [3] value     - LiteralValue[Data has been successfully updated.]
        // [4] negate    - BooleanValue[false]
        // [5] errorDesc - LiteralValue[]

        private readonly int IgnoreErrorIndex = 0;
        private readonly int selectorIndex;

        private readonly int additionalParms; // handled by derived classes

        public ControlCommand(Command command, int additionalParms)
            : this(command, 1, additionalParms)
        {
        }

        public ControlCommand(Command command, int selectorIndex, int additionalParms)
            : base(command)
        {
            this.selectorIndex = selectorIndex;
            this.additionalParms = additionalParms;
        }

        protected static int SelectionParmCount => 2;

        protected int LastSelectionParm => selectorIndex + SelectionParmCount;

        protected int ParmCount => LastSelectionParm + 1 + additionalParms;

        private ControlRuleValue ControlSelector => Command.Parameters[selectorIndex].Value as ControlRuleValue;

        protected virtual bool PreGenerate(StringBuilder builder)
        {
            builder.AppendCommentLine($"{GetType().Name} command generation", Verbosity.Diagnostic);
            builder.AppendCommentLine($"Ignoring first parm {Command.Parameters[IgnoreErrorIndex]}", Verbosity.Diagnostic);

            if (Command.Parameters.Count < ParmCount)
            {
                builder.AppendLine("not enough parameters");
                return false;
            }

            return true;
        }

        protected string GetComparisonExpression()
        {
            var selectorHelper = new ControlRuleHelper(Command, ControlSelector);
            return selectorHelper.GetComparisonExpression();
        }
    }
}
