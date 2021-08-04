﻿using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class Command
    {
        [XmlElement("ElementName")]
        public string Id { get; set; }

        [XmlElement("CommandType")]
        public string Type { get; set; }

        [XmlElement("CommandName")]
        public string Name { get; set; }

        [XmlElement("CommandOrd")]
        public string Order { get; set; }
    }
}
