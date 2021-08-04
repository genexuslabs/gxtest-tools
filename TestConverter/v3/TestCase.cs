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

        [XmlElement("Comandos Elemento")]
        public List<Command> Commands { get; set; }

        [XmlElement("ParameterAbstract")]
        public List<Parameter> Parameters { get; set; }

        [XmlElement("Param Literal")]
        public List<ParameterLiteralValue> LiteralValues { get; set; }

        [XmlElement("ParametersBoolean")]
        public List<ParameterBooleanValue> BooleanValues { get; set; }

        [XmlElement("ParametersGXObject")]
        public List<ParameterControlValue> ControlValues { get; set; }

        [XmlElement("ControlExportTC")]
        public List<ParameterControlData> ControlData { get; set; }
    }
}
