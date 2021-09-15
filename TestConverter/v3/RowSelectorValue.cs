using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class RowSelectorValue : ParameterValue
    {
        public RowSelectorValue()
            : base(ParmType.SelectionByRow)
        {
        }

        [XmlElement("ParameterOrdValuable")]
        public int ValueParmIndex { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name}[Row at parm[{ValueParmIndex - 1}]]";
        }
    }
}
