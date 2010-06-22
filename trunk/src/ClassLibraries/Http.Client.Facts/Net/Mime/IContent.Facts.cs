namespace Cavity.Net.Mime
{
    using System;
    using System.IO;
    using System.Net.Mime;
    using Xunit;

    public sealed class IContentFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IContent).IsInterface);
        }

        [Fact]
        public void is_IContentType()
        {
            Assert.True(typeof(IContentType).IsAssignableFrom(typeof(IContent)));
        }

        [Fact]
        public void IContentType_ContentType_get()
        {
            try
            {
                ContentType value = (new IContentDummy() as IContentType).ContentType;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IContent_Write_StreamWriter()
        {
            try
            {
                (new IContentDummy() as IContent).Write(null as StreamWriter);
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}