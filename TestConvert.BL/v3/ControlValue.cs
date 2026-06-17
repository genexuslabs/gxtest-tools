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
    public class ControlValue : ParameterValue
    {
        public ControlValue()
            : base(ParmType.Control)
        {
        }

        [XmlElement("GXControlGUID")]
        public string ControlId { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name}[{ControlId}]";
        }

        private ParameterControlData controlData = null;

        public ParameterControlData Data => controlData;


        public void AddControlData(IReadOnlyDictionary<string, ParameterControlData> dataStore)
        {
            if (controlData != null)
                throw new Exception($"Trying to add control data for value '{this}' over existing data '{controlData}'");

            ParameterControlData data = dataStore[ControlId];
            controlData = data ?? throw new Exception($"Trying to add null control data for value '{this}'");
        }
    }
}
