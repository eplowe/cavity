namespace Cavity.Net.Mime
{
    using System;
    using Xunit;

    public sealed class IMediaTypeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IMediaType).IsInterface);
        }

        [Fact]
        public void IContent_ToContent_TextReader()
        {
            try
            {
                var value = (new IMediaTypeDummy() as IMediaType).ToContent(null);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}