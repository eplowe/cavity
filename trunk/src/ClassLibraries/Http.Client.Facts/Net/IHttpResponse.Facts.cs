namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IHttpResponseFacts
    {
        [Fact]
        public void a_definition()
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
                var value = (new IHttpResponseDummy() as IHttpResponse).StatusLine;
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}