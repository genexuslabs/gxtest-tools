using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class ParameterControlValue : ParameterValue
    {
        [XmlElement("GXControlGUID")]
        public string ControlId { get; set; }
    }
}
