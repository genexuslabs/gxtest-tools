using System.Collections;
using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class Node : Element
    {
        [XmlElement("PageName")]
        public string Id { get; set; }

        [XmlElement("PageDesc")]
        public string Description { get; set; }

        [XmlElement("GXObjectType")]
        public string ObjectType { get; set; }

        [XmlElement("GXObjectName")]
        public string ObjectName { get; set; }

        #region AfterSerialize

        private SortedList outboundEdges = new SortedList();

        public void AfterSerialize(TestCase testCase)
        {

        }

        #endregion
    }
}
