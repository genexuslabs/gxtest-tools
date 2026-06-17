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
