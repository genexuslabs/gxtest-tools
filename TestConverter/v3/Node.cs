using System;
using System.Collections.Generic;
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

        public override string ToString()
        {
            return $"Node[{Id}]";
        }

        private SortedList<string, Edge> outboundEdges = new();

        internal void AddEdge(Edge edge)
        {
            if (edge.SourceNodeId != this.Id)
                throw new Exception($"Trying to add {edge} with SourceNodeId='{edge.SourceNodeId}' to {this} (different Id).");

            if (outboundEdges.ContainsKey(edge.Order))
                throw new Exception($"Trying to adde {edge} with a duplicated order '{edge.Order}' to {this}.");

            outboundEdges.Add(edge.Order, edge);
        }

        public IList<Edge> OutboundEdges => outboundEdges.Values;
    }
}
