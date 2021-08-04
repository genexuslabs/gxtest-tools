using CommandLine;
using System;
using System.IO;

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

                // Load from XML file
                if (!converter.LoadFromXML())
                    return ErrorCode.ConversionError;

                // Output Code
                if (!converter.CreateTestCode())
                    return ErrorCode.ConversionError;

                ShowTestCode(converter.TestCode);
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
