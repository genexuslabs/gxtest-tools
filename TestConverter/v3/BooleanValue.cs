using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class BooleanValue : ParameterValue
    {
        public BooleanValue()
            : base(ParmType.Boolean)
        {
        }

        [XmlElement("ParameterBool")]
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name}[{Value}]";
        }
    }
}
