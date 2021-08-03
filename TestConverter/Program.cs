using GeneXus.GXtest.Tools.TestConverter.CommandLine;
using System;

namespace GeneXus.GXtest.Tools.TestConverter
{
    class Program
    {
        private string sourceFilePath;

        static int Main(string[] args)
        {
            // Parse command line arguments
            ProgramArguments parser = new ProgramArguments();
            try
            {
                parser.Parse(args);
            }
            catch (CommandLineParser.UsageException ex)
            {
                Console.Error.WriteLine(parser.GetUsage(ex.Message));
                return ReturnCode.BadParameters;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return ReturnCode.BadParameters;
            }

            // Execute conversion
            try
            {
                ExecuteConversion(parser);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
#if DEBUG
                Console.Error.WriteLine(ex.StackTrace);
#endif
                return ReturnCode.GenericError; // generic error
            }

            return ReturnCode.Success;
        }

        private static void ExecuteConversion(ProgramArguments parser)
        {
            throw new NotImplementedException();
        }
    }
}
