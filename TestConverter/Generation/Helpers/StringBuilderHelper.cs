﻿using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation
{
    internal static class StringBuilderHelper
    {
        private static string commentPrefix = "// ";
        public static StringBuilder AppendCommentLine(this StringBuilder builder, string text, Verbosity verbosityLevel = Verbosity.Normal)
        {
            if (verbosityLevel > GenerationOptions.Verbosity)
                return builder;

            return builder.AppendLine(commentPrefix + text);
        }

        public static StringBuilder AppendQuoted(this StringBuilder builder, string text)
        {
            return builder.Append($"\"{text}\"");
        }

        public static StringBuilder AppendQuoted(this StringBuilder builder, object obj)
        {
            return builder.AppendQuoted(obj.ToString());
        }
    }
}