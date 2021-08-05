using GeneXus.GXtest.Tools.TestConverter.v3;
using System;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter
{
    internal static class TestCodeGenerator
    {
        public static string Generate(TestCase testCase)
        {
            StringBuilder builder = new StringBuilder();

            GenerateHeader(builder, testCase);
            GenerateCommands(builder, testCase);

            return builder.ToString();
        }

        private static void GenerateHeader(StringBuilder builder, TestCase testCase)
        {
            builder.AppendLine($"// {testCase.GeneralData.Name}");
            builder.AppendLine("// Converted from GXtest v3");
            builder.AppendLine();

            builder.AppendLine("// Start webdriver");
            builder.AppendLine("&driver.Start()");
            builder.AppendLine("&driver.Maximize()");
        }

        private static void GenerateCommands(StringBuilder builder, TestCase testCase)
        {
            foreach (Element element in TestCaseTraverser.GetElements(testCase))
            {
                builder.AppendLine($"// {element}");
                GenerateElementCommands(builder, testCase, element);
            }
        }

        private static void GenerateElementCommands(StringBuilder builder, TestCase testCase, Element element)
        {
            foreach (Command command in element.GetCommands())
            {
                builder.AppendLine($"// {command}");

            }
        }
    }
}
