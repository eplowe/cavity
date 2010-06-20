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
        public void IHttpRequest_ToResponse_IHttpClient()
        {
            try
            {
                IHttpResponse value = (new IHttpRequestDummy() as IHttpRequest).ToResponse(null as IHttpClient);
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}