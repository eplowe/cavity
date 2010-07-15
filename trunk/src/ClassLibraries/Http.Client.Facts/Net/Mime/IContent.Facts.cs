namespace Cavity.Net.Mime
{
    using System;
    using Xunit;

    public sealed class IContentFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IContent).IsInterface);
        }

        [Fact]
        public void is_IContentType()
        {
            Assert.True(typeof(IContent).Implements(typeof(IContentType)));
        }

        [Fact]
        public void IContent_Write_TextWriter()
        {
            try
            {
                (new IContentDummy() as IContent).Write(null);
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}