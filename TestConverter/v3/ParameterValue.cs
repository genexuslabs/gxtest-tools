namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class ParameterValue : ParameterComponent
    {
        private readonly string type;

        public string Type => type;

        protected ParameterValue(string type)
        {
            this.type = type;
        }
    }
}
