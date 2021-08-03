using System;
using static GeneXus.GXtest.Tools.TestConverter.Helpers.CommandLineParser;

namespace GeneXus.GXtest.Tools.TestConverter
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int argumentsError = ParseArguments(args);
            if (argumentsError != ReturnCode.Success)
                return argumentsError;


            return ReturnCode.Success;
        }

        private static int ParseArguments(string[] args)
        {
            TestConverterCommandLineParser parser = new TestConverterCommandLineParser();
            try
            {
                parser.Parse(args);
            }
            catch (UsageException ex)
            {
                Console.Error.WriteLine(parser.GetUsage(ex.Message));
                return ReturnCode.BadParameters;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return ReturnCode.BadParameters;
            }

            return 0;
        }
    }
}
