using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class ParameterVariableValue : ParameterValue
    {
        public ParameterVariableValue()
            : base(ParameterTypes.Variable)
        {
        }

        [XmlElement("VariableName")]
        public string VariableName { get; set; }

        public override string ToString()
        {
            return $"ParameterVariableValue[&{VariableName}]";
        }
    }
}
