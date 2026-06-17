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
using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConvert.BL.v3
{
    public class Parameter : ParameterComponent
    {
        [XmlElement("ParameterType")]
        public ParmType Type { get; set; }

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

            if (val.Type != this.Type)
                throw new Exception($"Trying to add parameter value of type '{val.Type}' to a parameter of type {this.Type}");

            value = val;
        }

        public ParameterValue Value => value;

        #region Factory

        public static Parameter CreateParameter(ParmType parmType, ParameterValue parmValue)
        {
            var parm = new Parameter
            {
                Type = parmType
            };

            parm.AddValue(parmValue);

            return parm;
        }

        public static Parameter CreateLiteralParameter(string literal)
        {
            var literalValue = new LiteralValue
            {
                Value = literal
            };

            return CreateParameter(ParmType.Literal, literalValue);
        }

        public static Parameter CreateBooleanParameter(string boolean)
        {
            var booleanValue = new BooleanValue
            {
                Value = boolean
            };

            return CreateParameter(ParmType.Boolean, booleanValue);
        }

        #endregion
    }
}
