using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    class Edge
    {
        [XmlElement("EdgeName")]
        public string Id { get; set; }

        [XmlElement("EdgeDesc")]
        public string Description { get; set; }

        [XmlElement("PageNameSource")]
        public string SourceNode { get; set; }

        [XmlElement("PageNameTarget")]
        public string TargetNode { get; set; }

        [XmlElement("EdgeOrder")] 
        public string Order { get; set; }
    }
}
