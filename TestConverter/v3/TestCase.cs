using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    [XmlRoot("DSElements", Namespace = "http://tempuri.org/DSElements.xsd")]
    public class TestCase
    {
        [XmlElement("TCToXML")]
        public GeneralData GeneralData { get; set; }

        [XmlElement("Paginas")]
        public List<Node> Nodes { get; set; }

        [XmlElement("Aristas")]
        public List<Edge> Edges { get; set; }

        [XmlElement("Comandos_x0020_Elemento")]
        public List<Command> Commands { get; set; }
    }
}
