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
            public string SourceFilePath { get; set; }
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
            try
            {
                Console.Out.WriteLine($"Converting '{options.SourceFilePath}'");

                Converter converter = new Converter(options.SourceFilePath);
                if (!converter.Convert())
                    return ErrorCode.ConversionError;

                ShowTestCode(converter.GetTestCode());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
#if DEBUG
                Console.Error.WriteLine(ex.StackTrace);
#endif
                return ErrorCode.GenericError;
            }

            return ErrorCode.None;
        }

        private static void ShowTestCode(string code)
        {
            Console.Out.WriteLine(code);
        }
    }
}
