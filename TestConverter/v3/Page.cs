﻿using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class Page
    {
        [XmlElement("PageName")]
        public string Id { get; set; }

        [XmlElement("PageDesc")]
        public string Description { get; set; }
    }
}
