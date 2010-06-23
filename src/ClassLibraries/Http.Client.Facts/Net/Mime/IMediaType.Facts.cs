namespace Cavity.Net.Mime
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
        public void IContent_ToContent_TextReader()
        {
            try
            {
                IContent value = (new IMediaTypeDummy() as IMediaType).ToContent(null as TextReader);
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}