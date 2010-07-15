namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IUserAgentFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IUserAgent).IsInterface);
        }

        [Fact]
        public void IUserAgent_Value_get()
        {
            try
            {
                var value = (new IUserAgentDummy() as IUserAgent).Value;
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}