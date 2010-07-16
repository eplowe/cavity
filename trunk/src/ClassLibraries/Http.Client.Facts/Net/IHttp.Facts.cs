namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IHttpFacts
    {
        [Fact]
        public void IHttp_Send_IHttpRequest()
        {
            try
            {
                var value = (new IHttpDummy() as IHttp).Send(null);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IHttp).IsInterface);
        }
    }
}