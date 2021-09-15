using System;
using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
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
