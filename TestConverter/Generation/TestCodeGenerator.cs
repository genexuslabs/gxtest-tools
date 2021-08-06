using GeneXus.GXtest.Tools.TestConverter.Generation.Commands;
using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation
{
    internal static class TestCodeGenerator
    {
        public static string Generate(TestCase testCase)
        {
            var builder = new StringBuilder();

            GenerateHeader(builder, testCase);
            GenerateCommands(builder, testCase);

            return builder.ToString();
        }

        private static void GenerateHeader(StringBuilder builder, TestCase testCase)
        {
            GenerationOptions.Verbosity = Verbosity.Detailed;

            builder.AppendCommentLine($"{testCase.GeneralData.Name}");
            builder.AppendCommentLine("Converted from GXtest v3", Verbosity.Detailed);
            builder.AppendLine();

            builder.AppendCommentLine("Start webdriver");
            builder.AppendDriverMethodNoParms("Start");
            builder.AppendDriverMethodNoParms( "Maximize");
            builder.AppendLine();
        }

        private static void GenerateCommands(StringBuilder builder, TestCase testCase)
        {
            foreach (Element element in TestCaseTraverser.GetElements(testCase))
            {
                builder.AppendCommentLine($"{element}", Verbosity.Diagnostic);
                GenerateElementCommands(builder, element);
            }
        }

        private static void GenerateElementCommands(StringBuilder builder, Element element)
        {
            foreach (Command command in element.GetCommands())
            {
                builder.AppendCommand(command);
            }
        }
    }
}
