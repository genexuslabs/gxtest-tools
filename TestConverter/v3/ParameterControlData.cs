using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class ParameterControlData
    {
        [XmlElement("GXControlGUID")]
        public string ControlId { get; set; }

        [XmlElement("GXObjectType")]
        public string ObjectType { get; set; }

        [XmlElement("GXObjectName")]
        public string ObjectName { get; set; }

        [XmlElement("GXControlName")]
        public string Name { get; set; }

        [XmlElement("GXControlClass")]
        public string Class { get; set; }
    }
}
