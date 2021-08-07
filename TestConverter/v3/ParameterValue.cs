namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class ParameterValue : ParameterComponent
    {
        public string Type { get; protected set; }

        protected ParameterValue(string type)
        {
            Type = type;
        }
    }
}
