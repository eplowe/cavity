namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IHttpResponseFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IHttpResponse).IsInterface);
        }

        [Fact]
        public void is_IHttpMessage()
        {
            Assert.True(typeof(IHttpResponse).Implements(typeof(IHttpMessage)));
        }

        [Fact]
        public void IHttpResponse_StatusLine_get()
        {
            try
            {
                StatusLine value = (new IHttpResponseDummy() as IHttpResponse).StatusLine;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}