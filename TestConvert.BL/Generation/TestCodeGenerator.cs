using GeneXus.GXtest.Tools.TestConvert.BL.Generation.Commands;
using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation
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
            builder.AppendCommentLine($"{testCase.GeneralData.Name}");
            builder.AppendCommentLine("Converted from GXtest v3", Verbosity.Detailed);
            builder.AppendLine();

            builder.AppendCommentLine("Start webdriver");
            builder.AppendDriverMethodNoParms("Start");
            builder.AppendDriverMethodNoParms("Maximize");
            builder.AppendLine();
        }

        private static void GenerateCommands(StringBuilder builder, TestCase testCase)
        {
            foreach (Element element in TestCaseTraverser.GetElements(testCase))
            {
                builder.AppendCommentLine($"{element}", Verbosity.Diagnostic);
                GenerateElementCommands(builder, element);
            }

            GenerateEndMethod(builder);
        }

        private static void GenerateEndMethod(StringBuilder builder)
        {
            if (!GenerationOptions.General.GenerateEndMethod)
                return;

            builder.AppendDriverMethodNoParms(MethodNames.End);
        }

        private static void GenerateElementCommands(StringBuilder builder, Element element)
        {
            if (element.GetCommands().Count > 0)
            {
                foreach (Command command in element.GetCommands())
                {
                    builder.AppendCommand(command);
                }

                if (GenerationOptions.General.BlankLineAfterElement)
                    builder.AppendLine();
            }
        }
    }
}
