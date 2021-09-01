using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    abstract class TableCommand : CommandGenerator
    {
        //  [0] ignore        - ParameterBooleanValue[false],
        //  [1] grid          - ParameterControlValue[a4126e3c-555b-4570-86c5-da96ec7da727],
        //  [2] byRow         - /* SelectionByRow */ RowSelectorValue,
        //  [3] row           - ParameterLiteralValue[1],
        //  [4] targetControl - ParameterControlValue[401bbfb2-14de-4e7c-b79b-c2c0aaf12d0f],

        private readonly int IgnoreErrorIndex = 0;
        private readonly int GridIndex = 1;
        private readonly int selectorIndex = 2;

        private readonly int additionalParms; // handled by derived classes

        public TableCommand(Command command, int additionalParms)
            : base(command)
        {
            this.additionalParms = additionalParms;
        }

        public virtual bool PreGenerate(StringBuilder builder)
        {
            builder.AppendCommentLine($"{GetType().Name} command generation", Verbosity.Diagnostic);
            builder.AppendCommentLine($"Ignoring first parm {Command.Parameters[IgnoreErrorIndex]}", Verbosity.Diagnostic);
            builder.AppendCommentLine($"Ignoring grid parm {Command.Parameters[GridIndex]}", Verbosity.Diagnostic);

            if (SelectorType != ParmType.SelectionByRow)
            {
                builder.AppendLine("code not yet implemented");
                return false;
            }

            if (Command.Parameters.Count < ParmCount)
            {
                builder.AppendLine("not enough parameters");
                return false;
            }

            return true;
        }

        protected int SelectionParmCount => UsesRowSelector ? 1 : UsesControlSelector ? 2 : 1;

        private int LastSelectionParm => selectorIndex + SelectionParmCount;

        private int TargetControlIndex => LastSelectionParm + 1;

        protected int LastTableCommandParm => LastSelectionParm + 1; // base selection + TargetControl

        protected int ParmCount => LastTableCommandParm + 1 + additionalParms;

        protected ParmType SelectorType => Command.Parameters[selectorIndex].Type;

        protected bool UsesRowSelector => SelectorType == ParmType.SelectionByRow;

        protected bool UsesControlSelector => SelectorType == ParmType.SelectionByControl;

        protected bool UsesContextSelector => SelectorType == ParmType.SelectionByContext;

        private RowSelectorValue RowSelector => Command.Parameters[selectorIndex].Value as RowSelectorValue;
        private ControlSelectorValue ControlSelector => Command.Parameters[selectorIndex].Value as ControlSelectorValue;

        protected int Row
        {
            get
            {
                if (!UsesRowSelector || RowSelector == null)
                    return 0;

                return GetNumericValue(RowSelector.ValueParmIndex);
            }
        }

        protected string ControlName
        {
            get
            {
                if (!UsesControlSelector || ControlSelector == null)
                    return string.Empty;

                return ParameterHelper.GetParameterCode(Command.Parameters[ControlSelector.ControlParmIndex - 1]);
            }
        }

        protected ComparisonType ComparisonType => ControlSelector.ComparisonType;
        protected ComparisonOperator Comparator => ControlSelector.Comparator;

        protected int NumericValue
        {
            get
            {
                if (!UsesControlSelector || ControlSelector == null)
                    return 0;

                return GetNumericValue(ControlSelector.ValueParmIndex);
            }
        }

        protected string StringValue
        {
            get
            {
                if (!UsesControlSelector || ControlSelector == null)
                    return string.Empty;

                return GetStringValue(ControlSelector.ValueParmIndex);
            }
        }
            

        private int GetNumericValue(int parmIndex)
        {
            string numberAsString = StringHelper.RemoveQuotes(ParameterHelper.GetParameterCode(Command.Parameters[parmIndex - 1]));

            if (!int.TryParse(numberAsString, out int number))
                return 0;

            return number;
        }

        private string GetStringValue(int parmIndex)
        {
            return ParameterHelper.GetParameterCode(Command.Parameters[parmIndex - 1]);
        }

        protected string TargetControlName => ParameterHelper.GetParameterCode(Command.Parameters[TargetControlIndex]);
    }
}
