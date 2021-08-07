using GeneXus.GXtest.Tools.TestConverter.v3;
using System;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConverter.Generation.Parameters
{
    abstract class ParameterGenerator
    {
        protected Parameter Parameter { get; set; }

        protected ParameterGenerator(Parameter parm)
        {
            Parameter = parm;
        }

        protected static void ValidateParameterTypes(Parameter parm, string parmType, Type valueType)
        {
            if (parm.Type != parmType)
                throw new Exception($"Invalid parameter type. Expected {parmType}; found {parm.Type}");

            if (parm.Value == null)
                throw new Exception("Parameter contains no value");


            if (parm.Value.GetType() != valueType)
                throw new Exception($"Parameter contains invalid value. Expected {valueType}; found {parm.Value.GetType()}.");
        }


        public string ToCode()
        {
            var builder = new StringBuilder();
            Generate(builder);
            return builder.ToString();
        }

        public abstract void Generate(StringBuilder builder);
    }
}
