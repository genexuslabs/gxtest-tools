using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public enum ParmType
    {
        [XmlEnum(Name = "Boolean")]
        Boolean,
        [XmlEnum(Name = "ControlGX")]
        Control,
        [XmlEnum(Name = "Value")]
        Literal,
        [XmlEnum(Name = "SelectionByControl")]
        SelectionByControl,
        [XmlEnum(Name = "SelectionByRow")]
        SelectionByRow,
        [XmlEnum(Name = "Variable")]
        Variable
    }
}
