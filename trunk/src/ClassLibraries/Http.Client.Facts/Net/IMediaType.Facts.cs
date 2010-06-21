namespace Cavity.Net
{
    using System;
    using System.IO;
    using Xunit;

    public sealed class IMediaTypeFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IMediaType).IsInterface);
        }

        [Fact]
        public void IHttpBody_ToBody_StreamReader()
        {
            try
            {
                IHttpBody value = (new IMediaTypeDummy() as IMediaType).ToBody(null as StreamReader);
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}