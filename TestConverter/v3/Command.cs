using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class Command
    {
        [XmlElement("ElementName")]
        public string ParentId { get; set; }

        [XmlElement("CommandOrd")]
        public string Order { get; set; }

        [XmlElement("CommandType")]
        public string Type { get; set; }

        [XmlElement("CommandName")]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name}({string.Join(", ", Parameters)})";
        }

        private SortedList<string, Parameter> parms = new();

        internal IList<Parameter> Parameters => parms.Values;

        internal void AddParameter(Parameter parm)
        {
            if (parm.ParentId != this.ParentId)
                throw new Exception($"Trying to add parameter for a command in element '{parm.ParentId}' to a command in element '{this.ParentId}'");

            if (parm.ParentOrder != this.Order)
                throw new Exception($"Trying to add parameter for a command in position '{parm.ParentOrder}' to a command in position '{this.Order}'");

            parms.Add(parm.Order, parm);
        }

        internal void AddParameterValue(ParameterValue val)
        {
            if (val.ParentId != this.ParentId)
                throw new Exception($"Trying to add parameter value for a command in element '{val.ParentId}' to a command in element '{this.ParentId}'");

            if (val.ParentOrder != this.Order)
                throw new Exception($"Trying to add parameter value for a command in position '{val.ParentOrder}' to a command in position '{this.Order}'");

            parms[val.Order].AddValue(val);
        }
    }
}
