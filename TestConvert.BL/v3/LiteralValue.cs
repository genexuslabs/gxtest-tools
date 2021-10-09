using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConvert.BL.v3
{
    public class LiteralValue : ParameterValue
    {
        public LiteralValue()
            : base(ParmType.Literal)
        {
        }

        [XmlElement("ParameterLiteralValue")]
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name}[{Value}]";
        }
    }
}
