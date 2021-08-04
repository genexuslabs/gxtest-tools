using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class ParameterComponent
    {
        [XmlElement("ElementName")]
        public string ParentId { get; set; }

        [XmlElement("CommandOrd")]
        public string ParentOrder { get; set; }

        [XmlElement("ParameterOrd")]
        public string Order { get; set; }
    }
}
