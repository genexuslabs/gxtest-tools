using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConvert.BL.v3
{
    public class Edge : Element
    {
        [XmlElement("EdgeName")]
        public string Id { get; set; }

        [XmlElement("EdgeDesc")]
        public string Description { get; set; }

        [XmlElement("PageNameSource")]
        public string SourceNodeId { get; set; }

        [XmlElement("PageNameTarget")]
        public string TargetNodeId { get; set; }

        [XmlElement("EdgeOrder")]
        public string Order { get; set; }

        public override string ToString()
        {
            return $"Edge[{Id}]";
        }

        public Node SourceNode { get; set; }

        public Node TargetNode { get; set; }
    }
}
