using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public enum ComparisonOperator
    {
        #region Number Comparison
        [XmlEnum(Name = "minor")]
        LessThan,

        [XmlEnum(Name = "mayor")]
        GreaterThan,

        [XmlEnum(Name = "equal")]
        Equal,

        [XmlEnum(Name = "different")]
        NotEqual,

        [XmlEnum(Name = "equal")]
        Equals, //also used on strings

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
