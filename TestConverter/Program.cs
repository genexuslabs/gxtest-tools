using CommandLine;
using System;

namespace GeneXus.GXtest.Tools.TestConverter
{
    class Program
    {
        [Verb("convert", true, HelpText = "Convert a GXtest 3.0 XML test case to GXtest 4.0")]
        public class ConvertOptions
        {
            [Option('s', "source", Required = true, HelpText = "Path to the source XML file")]
            public string sourceFilePath { get; set; }

        }

        static int Main(string[] args)
        {
            ErrorCode result = Parser.Default.ParseArguments<ConvertOptions>(args).MapResult(
            options => Convert(options),
            _ => ErrorCode.BadParameters);

            return (int) result;
        }

        static ErrorCode Convert(ConvertOptions options)
        {
            Console.Out.Write($"Converting {options.sourceFilePath}");
            return ErrorCode.None;
        }
    }
}
