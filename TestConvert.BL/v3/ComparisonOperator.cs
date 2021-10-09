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
