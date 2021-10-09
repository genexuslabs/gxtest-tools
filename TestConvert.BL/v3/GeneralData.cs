using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConvert.BL.v3
{
    public class GeneralData
    {
        [XmlElement("TestCaseName")]
        public string Name { get; set; }

        [XmlElement("StartElementName")]
        public string StartId { get; set; }
    }
}
