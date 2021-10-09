using GeneXus.GXtest.Tools.TestConvert.BL.Generation.Helpers;
using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands
{
    class AppearBalloonTable : TableCommand
    {
        private const int AdditionalParms = 2; // negate, error

        public AppearBalloonTable(Command command)
            : base(command, AdditionalParms)
        {
            Debug.Assert(command.Name == CommandNames.AppearBalloonTable);
        }

        protected int NegateIndex => LastTableCommandParm + 1;
        protected int ErrorMsgIndex => NegateIndex + 1;

        public override void Generate(StringBuilder builder)
        {
            // Validation AppearBalloonTable(
            //  [0] ignore        - ParameterBooleanValue[false],
            //  [1] grid          - ParameterControlValue[a4126e3c-555b-4570-86c5-da96ec7da727],
            //  [2] byRow         - /* SelectionByRow */ RowSelectorValue,
            //  [3] row           - ParameterLiteralValue[1],
            //  [4] targetControl - ParameterControlValue[401bbfb2-14de-4e7c-b79b-c2c0aaf12d0f],
            //  [5] negate?       - ParameterBooleanValue[false],
            //  [6} errorMsg      - ParameterLiteralValue[No matching 'Country'.])

            if (!PreGenerate(builder))
                return;

            builder.AppendHasValidation(Command.Parameters[NegateIndex], Command.Parameters[ErrorMsgIndex], TargetControlName, Row);
        }

        protected override bool PreGenerate(StringBuilder builder)
        {
            if (!base.PreGenerate(builder))
                return false;

            if (!UsesRowSelector)
            {
                builder.AppendLine("code not yet implemented");
                return false;
            }

            return true;
        }
    }
}
