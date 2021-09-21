using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class ControlRuleValue : ParameterValue
    {
        public ControlRuleValue()
            : base(ParmType.SelectionByControl)
        {
        }

        [XmlElement("ParameterOrdControlGX")]
        public int ControlParmIndex { get; set; }

        [XmlElement("Comparator")]
        public ComparisonOperator Comparator { get; set; }

        [XmlElement("ComparatorType")]
        public ComparisonType ComparisonType { get; set; }

        [XmlElement("ParameterOrdValuable")]
        public int ValueParmIndex { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name}[Control at parm[{ControlParmIndex - 1}] {Comparator}(as {ComparisonType}) Value at parm[{ValueParmIndex - 1}]]";
        }
    }
}
