namespace Cavity.Xml
{
    using System;
    using System.Xml.Serialization;

    public class IXmlSerializerNamespacesDummy : IXmlSerializerNamespaces
    {
        public XmlSerializerNamespaces XmlNamespaceDeclarations
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}