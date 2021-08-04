using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class Parameter : ParameterComponent
    {
        [XmlElement("ParameterType")]
        public string Type { get; set; }
    }
}
