﻿using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class ParameterBooleanValue : ParameterValue
    {
        public ParameterBooleanValue()
            : base(ParameterTypes.Boolean)
        {
        }

        [XmlElement("ParameterBool")]
        public string Value { get; set; }

        public override string ToString()
        {
            return $"ParameterBooleanValue[{Value}]";
        }
    }
}
