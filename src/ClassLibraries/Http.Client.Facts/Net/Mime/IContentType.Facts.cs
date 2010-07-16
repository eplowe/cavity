namespace Cavity.Net.Mime
{
    using System;
    using Xunit;

    public sealed class IContentTypeFacts
    {
        [Fact]
        public void IContentType_ContentType_get()
        {
            try
            {
                var value = (new IContentTypeDummy() as IContentType).ContentType;
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IContentType).IsInterface);
        }
    }
}