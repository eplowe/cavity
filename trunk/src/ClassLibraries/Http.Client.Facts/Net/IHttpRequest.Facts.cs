namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IHttpRequestFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IHttpRequest).IsInterface);
        }

        [Fact]
        public void is_IHttpMessage()
        {
            Assert.True(typeof(IHttpRequest).Implements(typeof(IHttpMessage)));
        }

        [Fact]
        public void IHttpRequest_AbsoluteUri_get()
        {
            try
            {
                Uri value = (new IHttpRequestDummy() as IHttpRequest).AbsoluteUri;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IHttpRequest_RequestLine_get()
        {
            try
            {
                RequestLine value = (new IHttpRequestDummy() as IHttpRequest).RequestLine;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}