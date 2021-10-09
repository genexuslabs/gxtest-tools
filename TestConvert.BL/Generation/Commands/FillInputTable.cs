using GeneXus.GXtest.Tools.TestConvert.BL.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands
{
    class FillInputTable : TableCommand
    {
        private const int AdditionalParms = 1; // value to fill

        public FillInputTable(Command command)
            : base(command, AdditionalParms)
        {
            Debug.Assert(command.Name == CommandNames.FillInputTable);
        }

        protected int InputIndex => LastTableCommandParm + 1;

        public override void Generate(StringBuilder builder)
        {
            if (!PreGenerate(builder))
                return;

            string valueToType = ParameterHelper.GetParameterCode(Command.Parameters[InputIndex]);
            builder.AppendDriverMethod(MethodNames.Type, TargetControlName, RowExpression, valueToType);
        }

    }
}
