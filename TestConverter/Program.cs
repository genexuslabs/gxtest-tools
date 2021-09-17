using CommandLine;
using CommandLine.Text;
using GeneXus.GXtest.Tools.TestConverter.Generation;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GeneXus.GXtest.Tools.TestConverter
{
    class Program
    {
        [Verb("convert", true, HelpText = "Convert a GXtest 3.0 XML test case to GXtest 4.0")]
        public class ConvertOptions
        {
            [Option('f', "file", Required = true, HelpText = "Path to the source XML file.")]
            public string SourceFilePath { get; set; }

            [Option('v', "verbosity", Required = false, HelpText = "Verbosity level on code comments.")]
            public Verbosity Verbosity { get; set; } = Verbosity.Normal;

            [Option("vars", Separator = ';', Required = false, HelpText = "List of semicolon separated 'name=value' variable substitutions. Example: \"--vars\u00A0testmain=TestMain.Link()\".")]
            public IEnumerable<string> Variables { get; set; }

            [Option('c', "compact", Required = false, HelpText = "Generate a blank line to separate commands from different elements.")]
            public bool Compact { get; set; }

            [Option('m', "noEndMethod", Required = false, HelpText = "Don't generate a final '&driver.End()' line.")]
            public bool SuppressEndMethod { get; set; }

            [Option("debug", Required = false, Hidden = true, HelpText = "Launch debugger at startup.")]
            public bool LaunchDebugger { get; set; }
        }

        static int Main(string[] args)
        {
            var parser = new Parser(settings =>
            {
                settings.CaseInsensitiveEnumValues = true;
                settings.CaseSensitive = false;
                settings.HelpWriter = null;
            });

            var parserResult = parser.ParseArguments<ConvertOptions>(args);

            ErrorCode result = parserResult.MapResult(
                options =>
                {
                    return Convert(options);
                },
                errors =>
                {
                    DisplayHelp(parserResult);
                    return ErrorCode.BadParameters;
                });

            return (int)result;
        }

        static void DisplayHelp<T>(ParserResult<T> result)
        {
            var helpText = HelpText.AutoBuild(result, h =>
            {
                h.AdditionalNewLineAfterOption = false;
                h.MaximumDisplayWidth = 120;
                h.AddEnumValuesToHelpText = true;
                return h;
            });
            Console.WriteLine(helpText);
        }

        static ErrorCode Convert(ConvertOptions options)
        {
            try
            {
                if (options.LaunchDebugger)
                    Debugger.Launch();

                Console.Out.WriteLine($"Converting '{options.SourceFilePath}'");
                GenerationOptions.General.Verbosity = options.Verbosity;
                GenerationOptions.General.SetVariables(options.Variables);
                GenerationOptions.General.BlankLineAfterElement = !options.Compact;
                GenerationOptions.General.GenerateEndMethod = !options.SuppressEndMethod;

                var converter = new Converter();
                if (!converter.ConvertFromFile(options.SourceFilePath))
                    return ErrorCode.ConversionError;

                ShowTestCode(converter.GetTestCode());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");

                if (GenerationOptions.General.Verbosity == Verbosity.Diagnostic)
                    Console.Error.WriteLine(ex.StackTrace);

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
