using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class ParameterBooleanValue : ParameterValue
    {
        [XmlElement("ParameterBool")]
        public string Value { get; set; }
    }
}
