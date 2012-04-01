namespace Cavity.Types
{
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.Serialization;

    public sealed class XmlDecorationClass2
    {
        [XmlArray("array1")]
        [XmlArrayItem(ElementName = "item", Type = typeof(Class1))]
        [XmlArrayItem(ElementName = "derived", Type = typeof(DerivedClass1))]
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "This is for testing purposes.")]
        public string[] Array1 { get; set; }
    }
}