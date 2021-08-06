using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation
{
    internal static class CommentHelper
    {
        private static string commentPrefix = "// ";
        public static void AppendCommentLine(this StringBuilder builder, string text, Verbosity verbosityLevel = Verbosity.Normal)
        {
            if (verbosityLevel > GenerationOptions.Verbosity)
                return;

            builder.AppendLine(commentPrefix + text);
        }
    }
}
