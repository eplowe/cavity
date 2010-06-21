namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IHttpFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IHttp).IsInterface);
        }

        [Fact]
        public void IHttp_Send_IHttpRequest()
        {
            try
            {
                IHttpResponse value = (new IHttpDummy() as IHttp).Send(null as IHttpRequest);
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}