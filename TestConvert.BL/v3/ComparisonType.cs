using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConvert.BL.v3
{
    public enum ComparisonType
    {
        [XmlEnum(Name = "ComparatorStr")]
        String,
        [XmlEnum(Name = "ComparatorNumber")]
        Number
    }
}
