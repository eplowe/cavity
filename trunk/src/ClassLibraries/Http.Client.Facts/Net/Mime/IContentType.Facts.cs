namespace Cavity.Net.Mime
{
    using System;
    using Xunit;

    public sealed class IContentTypeFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IContentType).IsInterface);
        }

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
    }
}