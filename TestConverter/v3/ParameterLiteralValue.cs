using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class ParameterLiteralValue : ParameterValue
    {
        [XmlElement("ParameterLiteralValue")]
        public string Value { get; set; }
    }
}
