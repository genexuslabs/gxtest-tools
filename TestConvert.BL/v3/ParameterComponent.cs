using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConvert.BL.v3
{
    public class ParameterComponent
    {
        [XmlElement("ElementName")]
        public string ParentId { get; set; }

        [XmlElement("CommandOrd")]
        public int ParentOrder { get; set; }

        [XmlElement("ParameterOrd")]
        public int Order { get; set; }
    }
}
