using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation
{
    internal static class CommandGenerator
    {
        public static string ToCode(this Command command)
        {
            var builder = new StringBuilder();
            command.GenerateCode(builder);
            return builder.ToString();
        }

        public static void GenerateCode(this Command command, StringBuilder builder)
        {
            builder.AppendCommentLine($"{command}", Verbosity.Detailed);
            switch (command.Name)
            {
                case "Go":
                    command.GenerateGoCommand(builder);
                    break;

                default:
                    command.GenerateUnknownCommand(builder);
                    break;
            }
        }

        private static void GenerateUnknownCommand(this Command command, StringBuilder builder)
        {
            builder.AppendLine("code not yet implemented");
        }

        private static void GenerateGoCommand(this Command command, StringBuilder builder)
        {
            builder.AppendCommentLine("GO command generation", Verbosity.Diagnostic);

            builder.AppendCommentLine($"Ignoring first parm {command.Parameters[0]}", Verbosity.Diagnostic);
            GenerateDriverMethod(builder, "Go", command.Parameters[1]);
        }

        private static string driverVar = "&driver";

        private static void GenerateDriverMethod(StringBuilder builder, string methodName, params Parameter[] parameters)
        {
            builder.Append($"{driverVar}.{methodName}(");
            foreach (var parm in parameters)
                GenerateParameter(builder, parm);
            builder.AppendLine(")");
        }

        private static void GenerateParameter(StringBuilder builder, Parameter parm)
        {
            builder.Append("the parameter value");
        }
    }
}
