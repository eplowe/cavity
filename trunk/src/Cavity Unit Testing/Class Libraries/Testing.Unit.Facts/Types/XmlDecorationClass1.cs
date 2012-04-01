namespace Cavity.Types
{
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.Serialization;

    [XmlRoot("root", Namespace = "urn:example.net")]
    public sealed class XmlDecorationClass1
    {
        [XmlArray("array1")]
        [XmlArrayItem("item1")]
        [XmlNamespaceDeclarations]
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "This is for testing purposes.")]
        public string[] Array1 { get; set; }

        [XmlArray("array2")]
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "This is for testing purposes.")]
        public string[] Array2 { get; set; }

        [XmlAttribute("attribute")]
        public string Attribute { get; set; }

        [XmlElement("element")]
        public string Element { get; set; }

        [XmlIgnore]
        public string Ignore { get; set; }

        [XmlAttribute("attribute", Namespace = "urn:example.org")]
        public string NamespaceAttribute { get; set; }

        [XmlElement("element", Namespace = "urn:example.org")]
        public string NamespaceElement { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}