namespace Cavity.Xml
{
    using System.Xml.Serialization;

    public interface IXmlSerializerNamespaces
    {
        XmlSerializerNamespaces XmlNamespaceDeclarations { get; }
    }
}