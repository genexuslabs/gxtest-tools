using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Collections.Generic;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Helpers
{
    internal class ControlSelectorHelper
    {
        private readonly Command command;
        private readonly ControlSelectorValue selector;

        public ControlSelectorHelper(Command command, ControlSelectorValue selector)
        {
            this.command = command;
            this.selector = selector;
        }

        private const string RowByNumberMethod = "GetRowByControlNumericValue";
        private const string RowByTextMethod = "GetRowByControlTextValue";
        private const string CompareNumbersMethod = "Compare";
        private const string CompareTextsMethod = "CompareTextValue";

        public string GetRowExpression(string gridControlName)
        {
            string method = selector.ComparisonType == ComparisonType.Number ? RowByNumberMethod : RowByTextMethod;

            // eg: GetRowByControlNumericValue(&driver, "Grid1", "CountryId", CompareKind.Equal, 2)
            return $"{method}({DriverHelper.DriverVar}, {gridControlName}, {ControlName}, {Comparator}, {Value})";
        }

        public string GetComparisonExpression()
        {
            string method = CompareTextsMethod;
            string parm1 = GetControlTextCode(ControlName);

            if (selector.ComparisonType == ComparisonType.Number)
            {
                method = CompareNumbersMethod;
                parm1 += ".ToNumeric()";
            }

            return $"{method}({parm1}, {Comparator}, {Value})";
        }

        private static string GetControlTextCode(string name)
        {
            // HACK. GetText() doesn't currently work for the ErrorViewer control, so we use a work-around.
            string lowerName = StringHelper.RemoveQuotes(name.ToLowerInvariant());
            if (lowerName == "errorviewer" || lowerName == "gxerrorviewer")
            {
                return "&driver.GetTextByCSS(\"div.gx-warning-message\")";
            }

            return $"&driver.GetText({name})";
        }

        private string ControlName => ParameterHelper.GetParameterCode(command.Parameters[selector.ControlParmIndex - 1]);

        private string Comparator => GetComparatorExpression(selector.Comparator);

        private object Value => selector.ComparisonType == ComparisonType.Number ? NumericValue : StringValue;

        protected int NumericValue => ParameterHelper.GetNumericValue(command, selector.ValueParmIndex - 1);

        protected string StringValue => ParameterHelper.GetStringValue(command, selector.ValueParmIndex - 1);

        private static readonly Dictionary<ComparisonOperator, string> numberComparator = new()
        {
            { ComparisonOperator.Equal, "CompareKind.Equal" },
            { ComparisonOperator.Greater, "CompareKind.Greater" },
            { ComparisonOperator.Less, "CompareKind.Less" },
            { ComparisonOperator.NotEqual, "CompareKind.NotEqual" },
        };

        private static readonly Dictionary<ComparisonOperator, string> stringComparator = new()
        {
            { ComparisonOperator.Contains, "TextComparison.Contains" },
            { ComparisonOperator.EndsWith, "TextComparison.EndsWith" },
            { ComparisonOperator.Equal, "TextComparison.Equal" },
            { ComparisonOperator.RegEx, "TextComparison.RegEx" },
            { ComparisonOperator.StartsWith, "TextComparison.StartsWith" }
        };

        private string GetComparatorExpression(ComparisonOperator comparator)
        {
            return selector.ComparisonType == ComparisonType.Number ? numberComparator[comparator] : stringComparator[comparator];
        }
    }
}
