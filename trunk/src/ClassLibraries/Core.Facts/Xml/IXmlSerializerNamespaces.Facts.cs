namespace Cavity.Xml
{
    using System;
    using Xunit;

    public sealed class IXmlSerializerNamespacesFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IXmlSerializerNamespaces).IsInterface);
        }

        [Fact]
        public void IXmlSerializerNamespaces_XmlNamespaceDeclarations_get()
        {
            try
            {
                var value = (new IXmlSerializerNamespacesDummy() as IXmlSerializerNamespaces).XmlNamespaceDeclarations;
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}