namespace Cavity.Net
{
    using System;
    using System.IO;
    using Xunit;

    public sealed class IHttpContentFacts
    {
        [Fact]
        public void IHttpContent_op_Write_Stream()
        {
            try
            {
                Stream stream = null;
                (new IHttpContentDummy() as IHttpContent).Write(stream);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IHttpContent_prop_Type_get()
        {
            try
            {
                var value = (new IHttpContentDummy() as IHttpContent).Type;
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IHttpContent).IsInterface);
        }
    }
}