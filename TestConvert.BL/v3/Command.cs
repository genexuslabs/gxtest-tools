// Copyright 2021 GeneXus S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConvert.BL.v3
{
    public class Command
    {
        [XmlElement("ElementName")]
        public string ParentId { get; set; }

        [XmlElement("CommandOrd")]
        public int Order { get; set; }

        [XmlElement("CommandType")]
        public string Type { get; set; }

        [XmlElement("CommandName")]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name}({string.Join(", ", Parameters)})";
        }

        private readonly SortedList<int, Parameter> parms = new SortedList<int, Parameter>();

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
