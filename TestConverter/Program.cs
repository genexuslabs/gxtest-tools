using CommandLine;
using GeneXus.GXtest.Tools.TestConverter.Generation;
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

            [Option('v', "verbosity", Required = false, HelpText = "Verbosity level on code comments")]
            public Verbosity Verbosity { get; set; } = Verbosity.Normal;
        }

        static int Main(string[] args)
        {
            var parser = new Parser(settings =>
            {
                settings.CaseInsensitiveEnumValues = true;
                settings.CaseSensitive = false;

            });
            ErrorCode result = parser.ParseArguments<ConvertOptions>(args).MapResult(
            options => Convert(options),
            _ => ErrorCode.BadParameters);

            return (int) result;
        }

        static ErrorCode Convert(ConvertOptions options)
        {
            try
            {
                Console.Out.WriteLine($"Converting '{options.SourceFilePath}'");
                GenerationOptions.Verbosity = options.Verbosity;

                var converter = new Converter(options.SourceFilePath);
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
