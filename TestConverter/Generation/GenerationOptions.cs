namespace GeneXus.GXtest.Tools.TestConverter.Generation
{
    public enum Verbosity
    {
        Quiet = 0, // The most minimal output
        Minimal = 1, // Relatively little output
        Normal = 2, // Standard output.This should be the default if verbosity level is not set
        Detailed = 3, // Relatively verbose, but not exhaustive
        Diagnostic = 4, // The most verbose and informative verbosity
    }

    internal class GenerationOptions
    {
        public static Verbosity Verbosity { get; set; } = Verbosity.Normal;
    }
}
