using GeneXus.GXtest.Tools.TestConverter.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter
{
    internal static class TestCodeGenerator
    {
        public static string Generate(TestCase testCase)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"// {testCase.GeneralData.Name}");
            builder.AppendLine();

            foreach (Element element in TestCaseTraverser.GetElements(testCase))
            {
                builder.AppendLine($"// {element}");
            }

            return builder.ToString();
        }
    }
}
