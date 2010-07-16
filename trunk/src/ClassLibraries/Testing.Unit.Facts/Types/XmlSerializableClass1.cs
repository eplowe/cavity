namespace Cavity.Types
{
    using System.Xml.Serialization;

    [XmlRoot("root", Namespace = "urn:example.net")]
    public sealed class XmlSerializableClass1
    {
        [XmlArray("array1")]
        [XmlArrayItem("item1")]
        [XmlNamespaceDeclarations]
        public string[] Array1 { get; set; }

        [XmlArray("array2")]
        public string[] Array2 { get; set; }

        [XmlAttribute("attribute")]
        public string Attribute { get; set; }

        [XmlElement("element")]
        public string Element { get; set; }

        [XmlIgnore]
        public string Ignore { get; set; }

        [XmlAttribute("attribute", Namespace = "urn:example.org")]
        public string NamespacedAttribute { get; set; }

        [XmlElement("element", Namespace = "urn:example.org")]
        public string NamespacedElement { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}