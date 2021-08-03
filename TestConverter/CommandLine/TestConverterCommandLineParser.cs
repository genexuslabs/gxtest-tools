namespace GeneXus.GXtest.Tools.TestConverter.CommandLine
{
    class TestConverterCommandLineParser : CommandLineParser
    {
        [ValueUsage("Source XML file path", Optional = false, AlternateName1 = "s")]
        public string sourceFilePath = string.Empty;

        [ValueUsage("Output XML file path", Optional = true, AlternateName1 = "o")]
        public string outputFilePath = "ConvertedTest.xml";
    }
}
