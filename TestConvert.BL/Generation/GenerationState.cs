namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation
{
    internal class GenerationState
    {
        private static GenerationState state;

        public static GenerationState State
        {
            get
            {
                if (state == null)
                    state = new GenerationState();

                return state;
            }
        }

        public static void Reset() => state = null;

        public bool OnPrompt { get; set; }
    }
}
