namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IHttpClientFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IHttpClient).IsInterface);
        }

        [Fact]
        public void IHttpClient_UserAgent_get()
        {
            try
            {
                string value = (new IHttpClientDummy() as IHttpClient).UserAgent;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}