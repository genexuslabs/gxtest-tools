using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConvert.BL.v3
{
    public class VariableValue : ParameterValue
    {
        public VariableValue()
            : base(ParmType.Variable)
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
