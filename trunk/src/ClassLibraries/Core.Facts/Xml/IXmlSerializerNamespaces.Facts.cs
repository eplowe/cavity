namespace Cavity.Xml
{
    using System;
    using System.Xml.Serialization;
    using Xunit;

    public sealed class IXmlSerializerNamespacesFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IXmlSerializerNamespaces).IsInterface);
        }

        [Fact]
        public void IXmlSerializerNamespaces_XmlNamespaceDeclarations_get()
        {
            try
            {
                XmlSerializerNamespaces value = (new IXmlSerializerNamespacesDummy() as IXmlSerializerNamespaces).XmlNamespaceDeclarations;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}