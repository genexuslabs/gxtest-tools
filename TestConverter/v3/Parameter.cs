using System;
using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class Parameter : ParameterComponent
    {
        [XmlElement("ParameterType")]
        public string Type { get; set; }

        public override string ToString()
        {
            if (value == null)
                return $"/* {Type} */ null";

            return value.ToString();
        }

        private ParameterValue value = null;

        internal void AddValue(ParameterValue val)
        {
            if (value != null)
                throw new Exception($"Trying to add parameter value '{val}' over an existing value '{value}'");

            value = val;
        }

        public ParameterValue Value
        {
            get { return value; }
        }

    }
}
