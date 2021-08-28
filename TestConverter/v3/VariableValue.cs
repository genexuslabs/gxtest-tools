using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class VariableValue : ParameterValue
    {
        public VariableValue()
            : base(ParameterTypes.Variable)
        {
        }

        [XmlElement("VariableName")]
        public string VariableName { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name}[&{VariableName}]";
        }
    }
}
