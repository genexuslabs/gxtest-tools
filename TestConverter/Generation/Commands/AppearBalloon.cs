using GeneXus.GXtest.Tools.TestConverter.Generation.Helpers;
using GeneXus.GXtest.Tools.TestConverter.Generation.Parameters;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Diagnostics;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Commands
{
    class AppearBalloon : CommandGenerator
    {
        public AppearBalloon(Command command)
            : base(command)
        {
            Debug.Assert(command.Name == CommandNames.AppearBalloon);
        }

        private static int TargetControlIndex => 1;
        private static int NegateIndex => 2;
        private static int ErrorMsgIndex => 3;

        private string TargetControlName => ParameterHelper.GetParameterCode(Command.Parameters[TargetControlIndex]);

        public override void Generate(StringBuilder builder)
        {
            // Validation AppearBalloon(
            //  [0] ignore        - BooleanValue[false],
            //  [1] targetControl - ControlValue[8ff7df6c-3a86-4e1f-8423-a506eebe727c],
            //  [2] negate?       - BooleanValue[true],
            //  [3] errorMsg      - LiteralValue[The value is not a valid number.])

            builder.AppendHasValidation(Command.Parameters[NegateIndex], Command.Parameters[ErrorMsgIndex], TargetControlName);
        }
    }
}
