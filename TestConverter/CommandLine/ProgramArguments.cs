namespace GeneXus.GXtest.Tools.TestConverter.CommandLine
{
    class ProgramArguments : CommandLineParser
    {
        [ValueUsage("Source XML file path", Optional = false, AlternateName1 = "s")]
        public string sourceFilePath = string.Empty;

        [ValueUsage("Output XML file path", Optional = false, AlternateName1 = "o")]
        public string outputFilePath = "ConvertedTest.xml";
    }
}
