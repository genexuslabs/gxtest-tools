using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Parameters
{
    class ControlParm : ParameterGenerator
    {
        private readonly ControlValue ControlValue;

        public ControlParm(Parameter parm)
           : base(parm)
        {
            ValidateParameterTypes(parm, ParmType.Control, typeof(ControlValue));
            ControlValue = parm.Value as ControlValue;
        }

        public override void Generate(StringBuilder builder)
        {
            _ = builder.AppendQuoted(ControlName);
        }

        private bool IsVariable
        {
            get
            {
                const string variableControlClass = "Variable";
                return string.Compare(ControlValue.Data.Class, variableControlClass, /* ignoreCase */ true) == 0;
            }
        }

        private string ControlName
        {
            get
            {
                return $"{(IsVariable ? "&" : "")}{ControlValue.Data.Name}";
            }
        }
    }
}
