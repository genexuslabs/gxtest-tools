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
    public enum ComparisonOperator
    {
        #region Number Comparison
        [XmlEnum(Name = "minor")]
        Less,

        [XmlEnum(Name = "mayor")]
        Greater,

        [XmlEnum(Name = "equal")]
        Equal, //also used on strings

        [XmlEnum(Name = "different")]
        NotEqual,

        #endregion

        #region String Comparison

        [XmlEnum(Name = "startWith")]
        StartsWith,

        [XmlEnum(Name = "endWith")]
        EndsWith,

        [XmlEnum(Name = "contains")]
        Contains,

        [XmlEnum(Name = "regex")]
        RegEx
        #endregion
    }
}
