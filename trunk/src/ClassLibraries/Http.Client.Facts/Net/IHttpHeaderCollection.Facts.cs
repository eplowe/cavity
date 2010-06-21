namespace Cavity.Net
{
    using System;
    using System.Net.Mime;
    using Xunit;

    public sealed class IHttpHeaderCollectionFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IHttpHeaderCollection).IsInterface);
        }

        [Fact]
        public void IHttpHeaderCollection_ContentType_get()
        {
            try
            {
                ContentType value = (new IHttpHeaderCollectionDummy() as IHttpHeaderCollection).ContentType;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IHttpHeaderCollection_opIndexer_string_get()
        {
            try
            {
                string value = (new IHttpHeaderCollectionDummy() as IHttpHeaderCollection)["name"];
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}