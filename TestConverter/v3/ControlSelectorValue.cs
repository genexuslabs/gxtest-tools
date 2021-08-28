using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class ControlSelectorValue : ParameterValue
    {
        public ControlSelectorValue()
            : base(ParameterTypes.SelectionByControl)
        {
        }

        [XmlElement("ParameterOrdControlGX")]
        public int ControlParmIndex { get; set; }

        [XmlElement("Comparator")]
        public string Comparator { get; set; }

        [XmlElement("ComparatorType")]
        public string ComparatorType { get; set; }

        [XmlElement("ParameterOrdValuable")]
        public int ValueParmIndex { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name}[Control at parm[{ControlParmIndex - 1}] {Comparator}(as {ComparatorType}) Value at parm[{ValueParmIndex - 1}]]";
        }
    }
}
